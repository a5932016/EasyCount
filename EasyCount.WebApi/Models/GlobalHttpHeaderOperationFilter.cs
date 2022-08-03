using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EasyCount.WebApi.Models
{
    public class GlobalHttpHeaderOperationFilter : IOperationFilter
    {
        private IOptions<AppSetting> _appConfiguration;

        public GlobalHttpHeaderOperationFilter(IOptions<AppSetting> appConfiguration)
        {
            _appConfiguration = appConfiguration;
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            //如果是Identity驗證方式，不需要介面添加x-token的輸入框
            if (_appConfiguration.Value.IsIdentityAuth)
                return;

            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }

            var actionAttrs = context.ApiDescription.ActionDescriptor.EndpointMetadata;
            var isAnony = actionAttrs.Any(a => a.GetType() == typeof(AllowAnonymousAttribute));

            //不是匿名，則添加默認的X-Token
            if (!isAnony)
            {
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = Define.TOKEN_NAME,
                    In = ParameterLocation.Header,
                    Description = "當前使用者token",
                    Required = false
                });
            }
        }
    }
}
