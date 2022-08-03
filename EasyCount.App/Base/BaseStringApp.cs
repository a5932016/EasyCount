using EasyCount.App.Interface;
using EasyCount.Repository.Core;
using EasyCount.Repository.Interface;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace EasyCount.App.Base
{
    /// <summary>
    /// 業務層基類，UnitWork用於事務操作，Repository用於普通資料庫操作
    /// <para>如用户管理：Class UserManagerApp:BaseApp&lt;User&gt;</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TDbContext"></typeparam>
    public class BaseStringApp<T, TDbContext> : BaseApp<T, TDbContext> where T : StringEntity where TDbContext : DbContext
    {
        public BaseStringApp(IUnitWork<TDbContext> unitWork, IRepository<T, TDbContext> repository, IAuth auth) : base(unitWork, repository, auth)
        {
        }

        /// <summary>
        /// 按id批量删除
        /// </summary>
        /// <param name="ids"></param>
        public virtual void Delete(string[] ids)
        {
            if (ids.Length == 0)
            {
                throw new EasyCountException(ExceptionCode.ID不可為空);
            }

            Repository.Delete(u => ids.Contains(u.Id));
        }

        /// <summary>
        /// 按ID取得
        /// </summary>
        /// <param name="id"></param>
        public T Get(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new EasyCountException(ExceptionCode.ID不可為空);
            }

            return Repository.FirstOrDefault(x => x.Id == id && !x.DeleteTime.HasValue);
        }

        public async Task<T> GetAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new EasyCountException(ExceptionCode.ID不可為空);
            }

            return await Repository.FirstOrDefaultAsync(x => x.Id == id && !x.DeleteTime.HasValue);
        }
    }
}
