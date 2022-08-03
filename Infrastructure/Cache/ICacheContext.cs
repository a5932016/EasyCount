using System;

namespace Infrastructure.Cache
{
    /// <summary>
    /// 緩存接口
    /// </summary>
    public interface ICacheContext
    {
        /// <summary>
        /// 獲取緩存
        /// </summary>
        /// <typeparam name="T">緩存對象類型</typeparam>
        /// <param name="key">鍵</param>
        /// <returns>緩存對象</returns>
        public abstract T Get<T>(string key);

        public abstract List<T> GetList<T>(string key);

        /// <summary>
        /// 增加緩存(List對象)
        /// </summary>
        /// <typeparam name="T">緩存對象類型</typeparam>
        /// <param name="key">鍵</param>
        /// <param name="t">緩存對象</param>
        /// <returns>true成功,false失敗</returns>
        public abstract bool AddList<T>(string key, T t, DateTime expire);

        /// <summary>
        /// 設置緩存
        /// </summary>
        /// <typeparam name="T">緩存對象類型</typeparam>
        /// <param name="key">鍵</param>
        /// <param name="t">緩存對象</param>
        /// <returns>true成功,false失敗</returns>
        public abstract bool Set<T>(string key, T t, DateTime expire);

        /// <summary>
        /// 移出一個緩存
        /// </summary>
        /// <param name="key">緩存key</param>
        /// <returns>true成功,false失敗</returns>
        public abstract bool Remove(string key);

        /// <summary>
        /// 移出一個緩存(List對象)
        /// </summary>
        /// <param name="key">緩存key</param>
        /// <returns>true成功,false失敗</returns>
        public abstract bool RemoveList<T>(string key, T t, DateTime expire);
    }
}