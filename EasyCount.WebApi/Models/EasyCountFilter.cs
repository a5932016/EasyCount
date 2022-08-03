using EasyCount.App.Apps.SysLogs;
using EasyCount.App.Interface;
using EasyCount.Repository.Domain;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;

namespace EasyCount.WebApi.Models
{
    public class EasyCountFilter : IActionFilter
    {
        private readonly IAuth _authUtil;
        private readonly SysLogApp _logApp;

        public EasyCountFilter(IAuth authUtil, SysLogApp logApp)
        {
            _authUtil = authUtil;
            _logApp = logApp;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var description =
                (Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor;

            var Controllername = description.ControllerName.ToLower();
            var Actionname = description.ActionName.ToLower();

            //匿名标识
            var authorize = description.MethodInfo.GetCustomAttribute(typeof(AllowAnonymousAttribute));
            if (authorize != null)
            {
                return;
            }

            if (!_authUtil.CheckLogin())
            {
                context.HttpContext.Response.StatusCode = 401;
                context.Result = new JsonResult(new Response
                {
                    Code = 401,
                    Message = "驗證失敗，請提供正確TOKEN"
                });

                return;
            }

            _logApp.Add(new SysLog
            {
                Content = $"使用者訪問",
                Href = $"{Controllername}/{Actionname}",
                CreateUserName = _authUtil.GetUserName(),
                CreateUserId = _authUtil.GetCurrentUser().User.Id,
                TypeName = "訪問日誌"
            });
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            return;
        }
    }
}
