using EasyCount.Repository;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using System.ComponentModel;

namespace EasyCount.App
{
    public class DbExtension
    {
        private List<DbContext> _contexts = new List<DbContext>();

        private IOptions<AppSetting> _appConfiguration;
        private IHttpContextAccessor _httpContextAccessor;

        public DbExtension(IOptions<AppSetting> appConfiguration, EasyCountDBContext easyCountDBContext, IHttpContextAccessor httpContextAccessor)
        {
            _appConfiguration = appConfiguration;
            _httpContextAccessor = httpContextAccessor;
            _contexts.Add(easyCountDBContext);  //如果有多個DBContext，可以按EasyCountDBContext同樣的方式添加到_contexts中
        }

        /// <summary>
        /// 取得資料庫一個表的所有屬性值和屬性描述
        /// </summary>
        /// <param name="moduleName">模塊名稱/表名</param>
        /// <returns></returns>
        public List<KeyDescription> GetProperties(string moduleName)
        {
            var result = new List<KeyDescription>();
            const string domain = "easycount.repository.domain.";
            IEntityType entity = null;
            _contexts.ForEach(u =>
            {
                entity = u.Model.GetEntityTypes()
                    .FirstOrDefault(u => u.Name.ToLower() == domain + moduleName.ToLower());
            });

            if (entity == null)
            {
                throw new Exception($"未能找到{moduleName}對應的實體類");
            }

            foreach (var property in entity.ClrType.GetProperties())
            {
                object[] objs = property.GetCustomAttributes(typeof(DescriptionAttribute), true);
                object[] browsableObjs = property.GetCustomAttributes(typeof(BrowsableAttribute), true);
                var description = objs.Length > 0 ? ((DescriptionAttribute)objs[0]).Description : property.Name;

                if (string.IsNullOrEmpty(description)) description = property.Name;

                //如果沒有BrowsableAttribute或[Browsable(true)]表示可見，其他均為不可見，需要前端配合顯示
                bool browsable = browsableObjs == null || browsableObjs.Length == 0 ||
                                 ((BrowsableAttribute)browsableObjs[0]).Browsable;

                var typeName = property.PropertyType.Name;
                if (Nullable.GetUnderlyingType(property.PropertyType) != null)
                {
                    typeName = Nullable.GetUnderlyingType(property.PropertyType).Name;
                }

                result.Add(new KeyDescription
                {
                    Key = property.Name,
                    Description = description,
                    Browsable = browsable,
                    Type = typeName
                });
            }

            return result;
        }

        /// <summary>
        /// 取得資料庫DbContext中所有實體名稱
        /// <para>注意！並不能取得資料庫中的所有表</para>
        /// </summary>
        public List<string> GetDbEntityNames()
        {
            var names = new List<string>();
            var models = _contexts.Select(u => u.Model);

            foreach (var model in models)
            {
                // Get all the entity types information contained in the DbContext class, ...
                var entityTypes = model.GetEntityTypes();
                foreach (var entityType in entityTypes)
                {
                    var tableNameAnnotation = entityType.GetAnnotation("Relational:TableName");
                    names.Add(tableNameAnnotation.Value.ToString());
                }
            }

            return names;
        }
    }
}
