using EasyCount.Repository.Core;
using EasyCount.Repository.Enum;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyCount.Repository.Domain
{
    [Table("UserAccount")]
    [Index(nameof(UserId), nameof(DeleteTime), Name = "IX_UserId", IsUnique = true)]
    [Index(nameof(Account), nameof(DeleteTime), Name = "IX_Account", IsUnique = true)]
    public partial class UserAccount : StringEntity
    {
        public UserAccount()
        {
            this.UserId = string.Empty;
            this.Account = string.Empty;
            this.Password = string.Empty;
            this.Status = (int)UserAccountStatusEnum.啟用;
        }

        /// <summary>
        /// 使用者ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 帳號
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 狀態
        /// </summary>
        public int Status { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
