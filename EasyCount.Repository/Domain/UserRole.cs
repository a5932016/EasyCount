using EasyCount.Repository.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyCount.Repository.Domain
{
    /// <summary>
    /// 使用者角色
    /// </summary>
    [Table("UserRole")]
    public partial class UserRole : StringEntity
    {
        public UserRole()
        {
            this.UserId = string.Empty;
            this.RoleId = string.Empty;
            this.CreateTime = DateTime.Now;
            this.CreateUserId = string.Empty;
            this.CreateUserName = string.Empty;
            this.DeleteTime = new Nullable<DateTime>();
            this.DeleteUserId = string.Empty;
            this.DeleteUserName = string.Empty;
        }

        /// <summary>
        /// 使用者ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
