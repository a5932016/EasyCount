using EasyCount.Repository.Core;
using EasyCount.Repository.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyCount.Repository.Domain
{
    /// <summary>
    /// 角色表
    /// </summary>
    [Table("Role")]
    public partial class Role : StringEntity
    {
        public Role()
        {
            this.Name = string.Empty;
            this.IsSys = true;
            this.Status = (int)RoleStatusEnum.啟用;
            this.CreateTime = DateTime.Now;
            this.CreateUserId = string.Empty;
            this.CreateUserName = string.Empty;
            this.UpdateTime = new Nullable<DateTime>();
            this.UpdateUserId = string.Empty;
            this.UpdateUserName = string.Empty;
            this.DeleteTime = new Nullable<DateTime>();
            this.DeleteUserId = string.Empty;
            this.DeleteUserName = string.Empty;
        }

        /// <summary>
        /// 角色名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否系統角色
        /// </summary>
        public bool IsSys { get; set; }

        /// <summary>
        /// 狀態
        public int Status { get; set; }
    }
}
