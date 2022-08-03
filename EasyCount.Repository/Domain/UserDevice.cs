using EasyCount.Repository.Core;
using EasyCount.Repository.Enum;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyCount.Repository.Domain
{
    /// <summary>
    /// 使用者裝置表
    /// </summary>
    [Table("UserDevice")]
    [Index(nameof(UserId), nameof(DeleteTime), Name = "IX_UserId", IsUnique = true)]
    public partial class UserDevice : StringEntity
    {
        public UserDevice()
        {
            this.UserId = string.Empty;
            this.Token = string.Empty;
            this.Device = string.Empty;
            this.Status = (int)UserDeviceStatusEnum.啟用;
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
        /// 使用者ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// TOKEN
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 裝置
        /// </summary>
        public string Device { get; set; }

        /// <summary>
        /// 狀態
        /// </summary>
        public int Status { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
