using EasyCount.App.Interface;
using EasyCount.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace EasyCount.App.Base
{
    public abstract class BaseApp<T, TDbContext> where T : class where TDbContext : DbContext
    {
        /// <summary>
        /// 普通資料庫操作
        /// </summary>
        protected IRepository<T, TDbContext> Repository;

        /// <summary>
        /// 用於事務操作
        /// </summary>
        protected IUnitWork<TDbContext> UnitWork;

        protected IAuth _auth;

        public BaseApp(IUnitWork<TDbContext> unitWork, IRepository<T, TDbContext> repository, IAuth auth)
        {
            UnitWork = unitWork;
            Repository = repository;
            _auth = auth;
        }

        public List<string> GetPermissionList()
        {
            var loginUser = _auth.GetCurrentUser();

            return loginUser.Permissions.Select(x => x.Id).ToList();
        }
    }
}
