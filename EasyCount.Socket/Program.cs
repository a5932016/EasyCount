using Autofac.Extensions.DependencyInjection;
using Infrastructure.Helpers;

namespace EasyCount.Socket
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.ClearProviders(); //�h���q�{��x
                    // logging.AddLog4Net();
                })
                .UseServiceProviderFactory(
                    new AutofacServiceProviderFactory()) //�N�q�{ServiceProviderFactory���w��AutofacServiceProviderFactory
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    var configuration = ConfigHelper.GetConfigRoot();
                    var httpHost = configuration["AppSetting:HttpHost"];

                    webBuilder.UseUrls(httpHost).UseStartup<Startup>();
                }
                );
    }
}
