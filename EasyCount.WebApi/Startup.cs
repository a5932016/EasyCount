using Autofac;
using EasyCount.App;
using EasyCount.Repository;
using EasyCount.WebApi.Models;
using Infrastructure;
using Infrastructure.Extensions.AutofacManager;
using Infrastructure.Helpers;
using Infrastructure.Middleware;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;

namespace EasyCount.WebApi
{
    public class Startup
    {
        public IHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var loggerFactory = LoggerFactory.Create(builder => { builder.AddLog4Net(); });
            ILogger logger = loggerFactory.CreateLogger<Startup>();

            // 添加MiniProfiler服務
            services.AddMiniProfiler(options =>
            {
                // 設定訪問分析結果URL的路由地址
                options.RouteBasePath = "/profiler";

                options.ColorScheme = StackExchange.Profiling.ColorScheme.Auto;
                options.PopupRenderPosition = StackExchange.Profiling.RenderPosition.BottomLeft;
                options.PopupShowTimeWithChildren = true;
                options.PopupShowTrivial = true;
                options.SqlFormatter = new StackExchange.Profiling.SqlFormatters.InlineFormatter();
                //  options.IgnoredPaths.Add("/swagger/");
            }).AddEntityFramework(); //顯示SQL語法語耗時

            //添加swagger
            services.AddSwaggerGen(option =>
            {
                foreach (var controller in GetControllers())
                {
                    var groupname = GetSwaggerGroupName(controller);

                    option.SwaggerDoc(groupname, new OpenApiInfo
                    {
                        Version = "v1",
                        Title = groupname,
                        Description = "by zihyan",
                    });
                }

                logger.LogInformation($"api doc basepath:{AppContext.BaseDirectory}");
                foreach (var name in Directory.GetFiles(AppContext.BaseDirectory, "*.*",
                    SearchOption.AllDirectories).Where(f => Path.GetExtension(f).ToLower() == ".xml"))
                {
                    option.IncludeXmlComments(name, includeControllerXmlComments: true);
                    // logger.LogInformation($"find api file{name}");
                }

                option.OperationFilter<GlobalHttpHeaderOperationFilter>(); // 添加httpHeader参数
            });

            services.Configure<AppSetting>(Configuration.GetSection("AppSetting"));

            services.AddControllers(option => { option.Filters.Add<EasyCountFilter>(); })
                .ConfigureApiBehaviorOptions(options =>
                {
                    //启动WebAPI自动模态验证，处理返回值
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var problems = new CustomBadRequest(context);

                        return new BadRequestObjectResult(problems);
                    };
                }).AddNewtonsoftJson(options =>
                {
                    //忽略循環引用
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                });

            services.AddMemoryCache();

            services.AddCors();

            //            var origins = new []
            //            {
            //                "http://localhost:1803",
            //                "http://localhost:52789"
            //            };
            //            if (Environment.IsProduction())
            //            {
            //                origins = new []
            //                {
            //                    "http://demo.openauth.net.cn:1803",
            //                    "http://demo.openauth.net.cn:52789"
            //                };
            //            }
            //            services.AddCors(option=>option.AddPolicy("cors", policy =>
            //                policy.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins(origins)));

            var dbtypes = ((ConfigurationSection)Configuration.GetSection("AppSetting:DbTypes")).GetChildren()
                .ToDictionary(x => x.Key, x => x.Value);
            var connectionString = Configuration.GetConnectionString("EasyCountDBContext");
            logger.LogInformation($"資料庫類型：{JsonHelper.Instance.Serialize(dbtypes)}，連接字串：{connectionString}");
            services.AddDbContext<EasyCountDBContext>();

            services.AddHttpClient();

            services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(Configuration["DataProtection"]));
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            AutofacExt.InitAutofac(builder);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLog4Net();

            if (env.IsDevelopment())
            {
                app.UseHttpsRedirection();
                app.UseDeveloperExceptionPage();
            }

            app.UseMiniProfiler();

            // 允許訪問根目錄下的靜態文件
            var staticfile = new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(AppContext.BaseDirectory),
                OnPrepareResponse = (ctx) =>
                {
                    // 可以在這為靜態文件添加HTTP頭訊息
                    ctx.Context.Response.Headers["Access-Control-Allow-Origin"] = "*";
                }
            };

            app.UseStaticFiles(staticfile);

            //todo:测试可以允许任意跨域，正式环境要加权限
            app.UseCors(builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseRouting();

            app.UseAuthentication();

            // 啟用日志追蹤和異常提醒
            app.UseLogMiddleware();

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.IndexStream = () =>
                    GetType().GetTypeInfo().Assembly.GetManifestResourceStream("ShuiNi.WebApi.htmlpage.html");

                foreach (var controller in GetControllers())
                {
                    var groupname = GetSwaggerGroupName(controller);

                    c.SwaggerEndpoint($"/swagger/{groupname}/swagger.json", groupname);
                }

                c.DocExpansion(DocExpansion.List); //默认展开列表
                c.OAuthClientId("EasyCount.WebApi"); //oauth客户端名称
                c.OAuthAppName("EasyCount認證"); // 描述
            });

            // 配置ServiceProvider
            AutofacContainerModule.ConfigServiceProvider(app.ApplicationServices);

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        /// <summary>
        /// 獲取控制器對應的swagger分組值
        /// </summary>
        private string GetSwaggerGroupName(Type controller)
        {
            var groupname = controller.Name.Replace("Controller", "");
            var apisetting = controller.GetCustomAttribute(typeof(ApiExplorerSettingsAttribute));
            if (apisetting != null)
            {
                groupname = ((ApiExplorerSettingsAttribute)apisetting).GroupName;
            }

            return groupname ?? string.Empty;
        }

        /// <summary>
        /// 獲取所有的控制器
        /// </summary>
        private List<Type> GetControllers()
        {
            Assembly asm = Assembly.GetExecutingAssembly();

            var controlleractionlist = asm.GetTypes()
                .Where(type => typeof(ControllerBase).IsAssignableFrom(type))
                .OrderBy(x => x.Name).ToList();

            return controlleractionlist;
        }
    }
}
