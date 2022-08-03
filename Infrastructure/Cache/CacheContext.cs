using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Cache
{
    public class CacheContext : ICacheContext
    {
        //加入Lock機制限定同一Key同一時間只有一個Callback執行
        private const string AsyncLockPrefix = "$$CacheAsyncLock#";
        private IMemoryCache _objCache;

        public CacheContext(IMemoryCache objCache)
        {
            _objCache = objCache;
        }

        public object GetAsyncLock(string key)
        {
            //取得每個Key專屬的鎖定對象（object）
            string asyncLockKey = AsyncLockPrefix + key;
            lock (_objCache)
            {
                if (_objCache.Get<object>(asyncLockKey) == null)
                {
                    _objCache.Set<object>(asyncLockKey, new object(), DateTime.Now.AddDays(1));
                }
            }

            return _objCache.Get<object>(asyncLockKey);
        }

        public T Get<T>(string key)
        {
            lock (GetAsyncLock(key))
            {
                return _objCache.Get<T>(key);
            }
        }

        public List<T> GetList<T>(string key)
        {
            lock (GetAsyncLock(key))
            {
                return _objCache.Get<List<T>>(key);
            }
        }

        public bool AddList<T>(string key, T t, DateTime expire)
        {
            lock (GetAsyncLock(key))
            {
                var obj = Get<List<T>>(key);
                if (obj == null)
                {
                    obj = new List<T>();
                }

                obj.Add(t);

                _objCache.Set(key, obj, new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(expire));   //過期時間

                return true;
            }
        }

        public bool Set<T>(string key, T t, DateTime expire)
        {
            lock (GetAsyncLock(key))
            {
                var obj = Get<T>(key);
                if (obj != null)
                {
                    Remove(key);
                }

                _objCache.Set(key, t, new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(expire));   //過期時間

                return true;
            }
        }

        public bool Remove(string key)
        {
            _objCache.Remove(key);
            return true;
        }

        public bool RemoveList<T>(string key, T t, DateTime expire)
        {
            lock (GetAsyncLock(key))
            {
                var obj = Get<List<T>>(key);
                if (obj == null)
                {
                    obj = new List<T>();
                }

                if (obj.Contains(t))
                {
                    obj.Remove(t);
                    _objCache.Set(key, obj, new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(expire));   //過期時間
                }

                return true;
            }
        }
    }
}
