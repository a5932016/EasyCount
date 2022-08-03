using Autofac;
using EasyCount.App;
using EasyCount.Repository;
using Infrastructure;
using Infrastructure.Extensions.AutofacManager;
using Infrastructure.Helpers;
using Infrastructure.Middleware;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;

namespace EasyCount.Socket
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

            services.Configure<AppSetting>(Configuration.GetSection("AppSetting"));

            services.AddControllers(option => { })
                .ConfigureApiBehaviorOptions(options =>
                {
                    // 禁用自动模态验证
                    // options.SuppressModelStateInvalidFilter = true;
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

            app.UseCors(builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseRouting();

            app.UseAuthentication();

            app.UseLogMiddleware();

            // <snippet_UseWebSockets>
            var webSocketOptions = new WebSocketOptions
            {
                KeepAliveInterval = TimeSpan.FromMinutes(2)
            };

            app.UseWebSockets(webSocketOptions);
            // </snippet_UseWebSockets>

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            // 配置ServiceProvider
            AutofacContainerModule.ConfigServiceProvider(app.ApplicationServices);
        }
    }
}
