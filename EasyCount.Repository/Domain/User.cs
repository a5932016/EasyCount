using EasyCount.Repository.Core;
using EasyCount.Repository.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyCount.Repository.Domain
{
    /// <summary>
    /// 用戶基本資料表
    /// </summary>
    [Table("User")]
    public partial class User : StringEntity
    {
        public User()
        {
            this.Name = string.Empty;
            this.Status = (int)UserStatusEnum.啟用;
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
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 狀態
        /// </summary>
        public int Status { get; set; }

        public virtual ICollection<UserAccount> UserAccounts { get; set; }

        public virtual ICollection<UserDevice> UserDevices { get; set; }

        public virtual ICollection<UserInfo> UserInfos { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
