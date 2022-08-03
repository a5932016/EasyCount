using EasyCount.App.Apps.Counters;
using EasyCount.App.Apps.Counters.Request;
using EasyCount.App.Apps.Counters.Response;
using Infrastructure;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyCount.WebApi.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "CounterWebApi")]
    [Route("api/[controller]/[action]")]
    public class CounterController : ControllerBase
    {
        private readonly CounterApp _app;

        public CounterController(CounterApp app)
        {
            _app = app;
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
        public async Task<Response<CounterView>> GetView(string ViewId)
        {
            var Result = new Response<CounterView>();

            try
            {
                Result = new Response<CounterView>
                {
                    Result = await _app.GetView(ViewId)
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

        [AllowAnonymous]
        public Response<CounterView> Add(AddCounterReq request)
        {
            var Result = new Response<CounterView>();

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

            return Result;
        }

        [AllowAnonymous]
        public Response<CounterView> Update(UpdateCounterReq request)
        {
            var Result = new Response<CounterView>();

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

            return Result;
        }
    }
}
