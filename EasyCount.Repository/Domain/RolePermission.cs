using EasyCount.Repository.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyCount.Repository.Domain
{
    /// <summary>
    /// 角色權限表
    /// </summary>
    [Table("RolePermission")]
    public partial class RolePermission : StringEntity
    {
        public RolePermission()
        {
            this.RoleId = string.Empty;
            this.PermissionId = string.Empty;
            this.CreateTime = DateTime.Now;
            this.CreateUserId = string.Empty;
            this.CreateUserName = string.Empty;
            this.DeleteTime = new Nullable<DateTime>();
            this.DeleteUserId = string.Empty;
            this.DeleteUserName = string.Empty;
        }

        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 權限ID
        /// </summary>
        public string PermissionId { get; set; }
    }
}
