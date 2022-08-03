using EasyCount.App.Apps.Counters.Request;
using EasyCount.App.Apps.Counters.Response;
using EasyCount.App.Base;
using EasyCount.Repository;
using EasyCount.Repository.Domain;
using EasyCount.Repository.Enum;
using EasyCount.Repository.Interface;
using Infrastructure;
using Infrastructure.Cache;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace EasyCount.App.Apps.Counters
{
    public class CounterApp : BaseStringApp<Counter, EasyCountDBContext>
    {
        private ICacheContext _cacheContext;

        public CounterApp(ICacheContext cacheContext, IUnitWork<EasyCountDBContext> unitWork, IRepository<Counter, EasyCountDBContext> repository)
            : base(unitWork, repository, null)
        {
            _cacheContext = cacheContext;
        }

        public async Task<Response<List<CounterView>>> Load(QueryCounterListReq request)
        {
            var result = new Response<List<CounterView>>();
            var objs = UnitWork.Find<Counter>(null);

            if (!string.IsNullOrWhiteSpace(request.key))
            {
                objs = objs.Where(x => x.Title.Contains(request.key));
            }

            result.Result = await objs.OrderByDescending(u => u.CreateTime)
                .Skip((request.page - 1) * request.limit)
                .Take(request.limit).Select(x => (CounterView)x).ToListAsync();

            return result;
        }

        public async Task<CounterView> Get(string Id)
        {
            if (string.IsNullOrWhiteSpace(Id))
                throw new EasyCountException(ExceptionCode.ID不可為空);

            var result = new CounterView();
            var cache = _cacheContext.Get<Counter>(Id);
            if (cache != null)
            {
                result = cache;
            }
            else
            {
                var counter = GetAsync(Id);
                result = await counter;
            }

            return result;
        }

        public async Task<CounterView> GetView(string ViewId)
        {
            if (string.IsNullOrWhiteSpace(ViewId))
                throw new EasyCountException(ExceptionCode.ID不可為空);

            var result = new CounterView();

            // 確認ViewId的Cache，如果無就新增，有就用Id取表
            var counterId = _cacheContext.Get<string>(ViewId);
            if (string.IsNullOrWhiteSpace(counterId))
            {
                var counter = await Repository.FirstOrDefaultAsync(x => x.ViewId == ViewId && !x.DeleteTime.HasValue);
                _cacheContext.Set<string>(ViewId, counter.Id, DateTime.Now.AddDays(1));
                result = counter;
            }
            else
            {
                var counter = _cacheContext.Get<Counter>(counterId);
                if (counter == null)
                {
                    counter = await Repository.FirstOrDefaultAsync(x => x.ViewId == ViewId && !x.DeleteTime.HasValue);
                    _cacheContext.Set<Counter>(counter.Id, counter, DateTime.Now.AddDays(1));
                }

                result = counter;
            }

            return result;
        }

        public void Add(AddCounterReq request)
        {
            Counter counter = request;
            counter.GenerateDefaultKeyVal();
            counter.CreateTime = DateTime.Now;
            Repository.Add(counter);

            _cacheContext.Set<Counter>(counter.Id, counter, DateTime.Now.AddDays(1));
            _cacheContext.Set<string>(counter.ViewId, counter.Id, DateTime.Now.AddDays(1));
        }

        public async void Update(UpdateCounterReq request)
        {
            DateTime dt = DateTime.Now;

            Repository.Update(u => u.Id == request.Id, u => new Counter
            {
                Title = request.Title,
                Description = request.Description,
                Count = request.Count,
                LimitCount = request.LimitCount,
                Status = request.Status,
                UpdateTime = dt,
            });

            Counter counter = await Get(request.Id);
            _cacheContext.Set<Counter>(counter.Id, counter, DateTime.Now.AddDays(1));
            _cacheContext.Set<string>(counter.ViewId, counter.Id, DateTime.Now.AddDays(1));
        }

        public async Task<int> AddCount(string Id)
        {
            var dt = DateTime.Now;
            var counter = _cacheContext.Get<Counter>(Id);
            if (counter == null)
                counter = await Get(Id);

            if (counter == null)
                throw new EasyCountException(ExceptionCode.查無資料);

            counter.Count = counter.Count + 1;
            counter.UpdateTime = dt;

            if (counter.Count > counter.LimitCount)
                throw new EasyCountException(ExceptionCode.超過限制數量);

            if (counter.Status != (int)CounterStatusEnum.啟用)
                throw new EasyCountException(ExceptionCode.已經完成);

            _cacheContext.Set<Counter>(counter.Id, counter, dt.AddDays(1));
            _cacheContext.Set<string>(counter.ViewId, counter.Id, dt.AddDays(1));

            return counter.Count;
        }

        public async Task<int> SubtractCount(string Id)
        {
            var dt = DateTime.Now;
            var counter = _cacheContext.Get<Counter>(Id);
            if (counter == null)
            {
                counter = await Get(Id);
            }

            counter.Count = counter.Count - 1;
            counter.UpdateTime = dt;

            if (counter.Count < 0)
                throw new EasyCountException(ExceptionCode.已經沒人了);

            if (counter.Status != (int)CounterStatusEnum.啟用)
                throw new EasyCountException(ExceptionCode.已經完成);

            _cacheContext.Set<Counter>(counter.Id, counter, dt.AddDays(1));
            _cacheContext.Set<string>(counter.ViewId, counter.Id, dt.AddDays(1));

            return counter.Count;
        }

        public async Task ResetCount(string Id)
        {
            var dt = DateTime.Now;
            var counter = _cacheContext.Get<Counter>(Id);
            if (counter == null)
            {
                counter = await Get(Id);
            }

            counter.Count = 0;
            counter.UpdateTime = dt;

            if (counter.Status != (int)CounterStatusEnum.啟用)
                throw new EasyCountException(ExceptionCode.已經完成);

            _cacheContext.Set<Counter>(counter.Id, counter, dt.AddDays(1));
            _cacheContext.Set<string>(counter.ViewId, counter.Id, dt.AddDays(1));
        }
    }
}