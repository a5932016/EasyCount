using EasyCount.App.Apps.Counters;
using EasyCount.App.Apps.Counters.Request;
using EasyCount.App.Apps.Counters.Response;
using EasyCount.App.Interface;
using Infrastructure;
using Infrastructure.Exceptions;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EasyCount.Mvc.Controllers
{
    public class CounterController : BaseController
    {
        private readonly CounterApp _app;

        public CounterController(IAuth authUtil, CounterApp app) : base(authUtil)
        {
            _app = app;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<Response<CounterView>> Get(string Id)
        {
            var Result = new Response<CounterView>();

            try
            {
                Result = new Response<CounterView>
                {
                    Result = await _app.Get(Id)
                };
            }
            catch (EasyCountException ex)
            {
                Result.Code = 500;
                Result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            catch (Exception ex)
            {
                Result.Code = 500;
                Result.Message = ex.InnerException?.Message ?? ex.Message;
            }

            return Result;
        }

        public string Add(AddCounterReq request)
        {
            var Result = new Response();

            try
            {
                _app.Add(request);
            }
            catch (EasyCountException ex)
            {
                Result.Code = 500;
                Result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            catch (Exception ex)
            {
                Result.Code = 500;
                Result.Message = ex.InnerException?.Message ?? ex.Message;
            }

            return JsonHelper.Instance.Serialize(Result);
        }

        public string Update(UpdateCounterReq request)
        {
            var Result = new Response();

            try
            {
                _app.Update(request);
            }
            catch (EasyCountException ex)
            {
                Result.Code = 500;
                Result.Message = ex.InnerException?.Message ?? ex.Message;
            }
            catch (Exception ex)
            {
                Result.Code = 500;
                Result.Message = ex.InnerException?.Message ?? ex.Message;
            }

            return JsonHelper.Instance.Serialize(Result);
        }

        public void Delete(string Id)
        {
            
        }
    }
}
