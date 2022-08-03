using EasyCount.Repository.Domain;
using Infrastructure;

namespace EasyCount.App.Apps.Counters.Request
{
    public class AddCounterReq
    {
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

        public static implicit operator AddCounterReq(Counter counter)
        {
            return counter.MapTo<AddCounterReq>();
        }

        public static implicit operator Counter(AddCounterReq view)
        {
            return view.MapTo<Counter>();
        }
    }
}
