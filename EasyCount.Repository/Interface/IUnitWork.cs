using Microsoft.EntityFrameworkCore;
using EasyCount.Repository.Core;
using System.Data.Common;
using System.Linq.Expressions;

namespace EasyCount.Repository.Interface
{
    /// <summary>
    /// 工作單元接口
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public interface IUnitWork<TDbContext> where TDbContext : DbContext
    {
        /// <summary>
        /// EF默認情況下，每調用一次SaveChanges()都會執行一個單獨的事物
        /// 本接口實現在一個事務中可以多次執行SaveChanges()方法
        /// </summary>
        void ExecuteWithTransaction(Action action);

        /// <summary>
        /// 返回DbContext,用於多線程等極端情況
        /// </summary>
        /// <returns></returns>
        DbContext GetDbContext();

        /// <summary>
        /// 返回一個單獨的實體，如果紀錄多於1個則取第一個
        /// </summary>
        T FirstOrDefault<T>(Expression<Func<T, bool>> exp = null) where T : class;

        /// <summary>
        /// 判斷指定條件的紀錄是否存在
        /// </summary>
        bool Any<T>(Expression<Func<T, bool>> exp) where T : class;

        IQueryable<T> Find<T>(Expression<Func<T, bool>> exp = null) where T : class;

        IQueryable<T> Find<T>(int pageindex = 1, int pagesize = 10, string orderby = "",
            Expression<Func<T, bool>> exp = null) where T : class;

        int Count<T>(Expression<Func<T, bool>> exp = null) where T : class;

        /// <summary>
        /// 新增對象，如果Id為空，則斥動創建默認Id
        /// </summary>
        void Add<T>(T entity) where T : BaseEntity;

        /// <summary>
        /// 批量新增對象，如果對象Id為空，則會自動創建默認Id
        /// </summary>
        void BatchAdd<T>(T[] entities) where T : BaseEntity;

        /// <summary>
        /// 更新一個實體的所有屬性
        /// </summary>
        void Update<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        /// <summary>
        /// 實現按需要只更新部分更新
        /// <para>如：Update&lt;User&gt;(u =>u.Id==1,u =>new User{Name="ok"})</para>
        /// <para>该方法内部自动调用了SaveChanges()，需要ExecuteWithTransaction配合才能实现事务控制</para>
        /// </summary>
        /// <param name="where">更新條件</param>
        /// <param name="entity">更新後的實體</param>
        void Update<T>(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity) where T : class;

        /// <summary>
        /// 批量刪除
        /// <para>該方法內部自動調用SaveChanges()，需要ExecuteWithTransaction配合才能實現事務控制</para>
        /// </summary>
        void Delete<T>(Expression<Func<T, bool>> exp) where T : class;

        void Save();

        /// <summary>
        /// 該方法不支持EF自帶的事物,需要ExecuteWithTransaction配合才能實現事務控制
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        int ExecuteSql(string sql);

        /// <summary>
        /// 使用SQL腳本查詢
        /// </summary>
        /// <typeparam name="T"> T為數據庫實體</typeparam>
        /// <returns></returns>
        IQueryable<T> FromSql<T>(string sql, params object[] parameters) where T : class;

        /// <summary>
        /// 使用SQL腳本查詢
        /// </summary>
        /// <typeparam name="T"> T為非數據庫實體，需要再DbContext中增加對應的DbQuery</typeparam>
        /// <returns></returns>
        IQueryable<T> Query<T>(string sql, params object[] parameters) where T : class;

        /// <summary>
        /// 執行儲存過程
        /// </summary>
        /// <param name="procName">存儲過程名稱</param>
        /// <param name="sqlParams">存儲果程參數</param>
        List<T> ExecProcedure<T>(string procName, params DbParameter[] sqlParams) where T : class;

        #region 異步接口

        Task<int> ExecuteSqlRawAsync(string sql);
        Task<int> SaveAsync();
        Task<int> CountAsync<T>(Expression<Func<T, bool>> exp = null) where T : class;
        Task<bool> AnyAsync<T>(Expression<Func<T, bool>> exp) where T : class;
        Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> exp) where T : class;

        #endregion
    }
}
