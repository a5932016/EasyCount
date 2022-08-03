using Microsoft.AspNetCore.Builder;

namespace Infrastructure.Middleware
{
    public static class ApplicationBuilderExtension
    {
        /// <summary>
        /// 注入日誌中間件
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseLogMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
        }
    }
}
