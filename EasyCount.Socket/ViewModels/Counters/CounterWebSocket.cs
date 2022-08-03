namespace EasyCount.Socket.ViewModels.Counters
{
    public class CounterWebSocket
    {
        public string Id { get; set; }

        public CounterWebSocketEventEnum Event { get; set; }
    }

    public enum CounterWebSocketEventEnum
    {
        加一 = 0,
        減一 = 1,
        重置 = 2,
    }
}
