using EasyCount.App.Apps.SysLogs.Request;
using EasyCount.App.Base;
using EasyCount.Repository;
using EasyCount.Repository.Domain;
using EasyCount.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EasyCount.App.Apps.SysLogs
{
    public class SysLogApp : BaseStringApp<SysLog, EasyCountDBContext>
    {
        public SysLogApp(IUnitWork<EasyCountDBContext> unitWork, IRepository<SysLog, EasyCountDBContext> repository) : base(unitWork, repository, null)
        {
        }

        /// <summary>
        /// 取得列表
        /// </summary>
        public async Task<TableData> Load(QuerySysLogListReq request)
        {
            var result = new TableData();
            var objs = UnitWork.Find<SysLog>(null);
            if (!string.IsNullOrEmpty(request.key))
            {
                objs = objs.Where(u => u.Content.Contains(request.key) || u.Id.Contains(request.key));
            }

            result.data = await objs.OrderByDescending(u => u.CreateTime)
                .Skip((request.page - 1) * request.limit)
                .Take(request.limit).ToListAsync();

            result.count = await objs.CountAsync();

            return result;
        }

        public void Add(SysLog obj)
        {
            //程序類型取入口應用的名稱，可以根據自己需要調整
            obj.Application = Assembly.GetEntryAssembly().FullName.Split(',')[0];
            Repository.Add(obj);
        }

        public void Update(SysLog obj)
        {
            UnitWork.Update<SysLog>(u => u.Id == obj.Id, u => new SysLog
            {
                //todo:要修改的字段賦值
            });
        }
    }
}
