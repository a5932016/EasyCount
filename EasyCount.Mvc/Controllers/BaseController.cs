using EasyCount.App.Interface;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EasyCount.Mvc.Controllers
{
    public class BaseController : Controller
    {
        protected IAuth _authUtil;

        public BaseController(IAuth authUtil)
        {
            _authUtil = authUtil;
        }
    }
}
