using EasyCount.App.Apps.Counters;
using EasyCount.App.Apps.Counters.Response;
using EasyCount.Repository.Domain;
using EasyCount.Socket.ViewModels.Counters;
using Infrastructure;
using Infrastructure.Cache;
using Infrastructure.Exceptions;
using Infrastructure.Extensions;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace EasyCount.Socket.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "CounterWebSocket")]
    [Route("socket/[controller]/[action]")]
    public class CounterController : Controller
    {
        private readonly CounterApp _app;
        private ICacheContext _cacheContext;

        public CounterController(ICacheContext cacheContext, CounterApp app)
        {
            _app = app;
            _cacheContext = cacheContext;
        }

        public async Task ConnectionWS()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                await Echo(webSocket);
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }

        private async Task Echo(WebSocket webSocket)
        {
            var counter = new Counter();
            var buffer = new byte[1024 * 4];
            var response = new byte[1024 * 4];
            var counterWebSocket = new CounterWebSocket();
            string Id = string.Empty;
            string key = String.Empty;
            var webSockets = new List<WebSocket>();

            var receiveResult = await webSocket.ReceiveAsync(
                new ArraySegment<byte>(buffer), CancellationToken.None);

            while (!receiveResult.CloseStatus.HasValue)
            {
                try
                {
                    counterWebSocket = JsonHelper.Instance.Deserialize<CounterWebSocket>(System.Text.Encoding.UTF8.GetString(buffer));
                    if (counterWebSocket == null)
                        throw new EasyCountException(ExceptionCode.JSON格式錯誤);

                    Id = counterWebSocket.Id;
                    key = $"{counterWebSocket.Id}-websocket";

                    switch (counterWebSocket.Event)
                    {
                        case CounterWebSocketEventEnum.加一:
                            await _app.AddCount(Id);
                            break;
                        case CounterWebSocketEventEnum.減一:
                            await _app.SubtractCount(Id);
                            break;
                        case CounterWebSocketEventEnum.重置:
                            await _app.ResetCount(Id);
                            break;
                    }

                    // 取得所有WebSocket
                    webSockets = _cacheContext.Get<List<WebSocket>>(key);
                    if (webSockets == null)
                    {
                        webSockets = new List<WebSocket>();
                        webSockets.Add(webSocket);
                    }

                    if(!webSockets.Contains(webSocket))
                        _cacheContext.AddList<WebSocket>(key, webSocket, DateTime.Now.AddDays(1));

                    counter = await _app.Get(Id);
                    response = (new Response<CounterView>() { Result = counter }).ToBytes();
                    Parallel.ForEach(webSockets, async (socket) =>
                    {
                        await socket.SendAsync(
                            new ArraySegment<byte>(response, 0, response.Length),
                            receiveResult.MessageType,
                            receiveResult.EndOfMessage,
                            CancellationToken.None);
                    });

                    receiveResult = await webSocket.ReceiveAsync(
                        new ArraySegment<byte>(buffer), CancellationToken.None);
                }
                catch (EasyCountException e)
                {
                    var errMessage = (new Response<string>() { 
                        Code = e.Code.HasValue ? e.Code.Value : (int)ExceptionCode.未註明錯誤, 
                        Message = e.Message }).ToBytes();
                    await webSocket.SendAsync(
                        new ArraySegment<byte>(errMessage, 0, errMessage.Length),
                        receiveResult.MessageType,
                        receiveResult.EndOfMessage,
                        CancellationToken.None);

                    receiveResult = await webSocket.ReceiveAsync(
                        new ArraySegment<byte>(buffer), CancellationToken.None);
                }
            }

            await webSocket.CloseAsync(
                receiveResult.CloseStatus.Value,
                receiveResult.CloseStatusDescription,
                CancellationToken.None);

            _cacheContext.RemoveList<WebSocket>(key, webSocket, DateTime.Now.AddDays(1));

            counter = await _app.Get(Id);
            _app.Update((Counter)counter);
        }
    }
}