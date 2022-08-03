using EasyCount.Repository.Domain;
using Infrastructure;

namespace EasyCount.App.Apps.Counters.Response
{
    public class CounterView
    {
        public string Id { get; set; }

        public string ViewId { get; set; }

        /// <summary>
        /// 名稱
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 數量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 限制數量
        /// </summary>
        public int LimitCount { get; set; }

        /// <summary>
        /// 狀態
        /// </summary>
        public int Status { get; set; }

        public static implicit operator CounterView(Counter counter)
        {
            return counter.MapTo<CounterView>();
        }

        public static implicit operator Counter(CounterView view)
        {
            return view.MapTo<Counter>();
        }
    }
}
