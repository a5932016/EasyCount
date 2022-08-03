using EasyCount.App.Base;
using EasyCount.Repository;
using EasyCount.Repository.Interface;
using EasyCount.Repository.Domain;
using Microsoft.EntityFrameworkCore;
using EasyCount.App.Apps.Applications.Request;

namespace EasyCount.App.Apps.Applications
{
    /// <summary>
    /// 應用管理
    /// </summary>
    public class ApplicationApp : BaseStringApp<Application, EasyCountDBContext>
    {
        public ApplicationApp(IUnitWork<EasyCountDBContext> unitWork, IRepository<Application, EasyCountDBContext> repository) : base(unitWork, repository, null)
        {
        }

        /// <summary>
        /// 新增應用
        /// </summary>
        /// <param name="Application"></param>
        public void Add(Application Application)
        {
            if (string.IsNullOrEmpty(Application.Id))
            {
                Application.Id = Guid.NewGuid().ToString();
            }

            Repository.Add(Application);
        }

        /// <summary>
        /// 更新應用
        /// </summary>
        /// <param name="Application"></param>
        public void Update(Application Application)
        {
            Repository.Update(Application);
        }

        /// <summary>
        /// 取得應用列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<Application>> GetList(QueryAppListReq request)
        {
            var applications = UnitWork.Find<Application>(null);

            return await applications.ToListAsync();
        }

        /// <summary>
        /// 取得應用 OF Key
        /// </summary>
        /// <param name="modelAppKey"></param>
        /// <returns></returns>
        public Application GetByAppKey(string modelAppKey)
        {
            return Repository.FirstOrDefault(u => u.AppSecret == modelAppKey);
        }
    }
}
