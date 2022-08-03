using EasyCount.Repository.Domain;
using Infrastructure;

namespace EasyCount.App.Apps.Counters.Request
{
    public class UpdateCounterReq : AddCounterReq
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }

        public static implicit operator UpdateCounterReq(Counter counter)
        {
            return counter.MapTo<UpdateCounterReq>();
        }

        public static implicit operator Counter(UpdateCounterReq view)
        {
            return view.MapTo<Counter>();
        }
    }
}
