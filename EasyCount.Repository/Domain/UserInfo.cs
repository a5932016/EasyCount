using EasyCount.Repository.Core;
using EasyCount.Repository.Enum;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyCount.Repository.Domain
{
    /// <summary>
    /// 使用者
    /// </summary>
    [Table("UserInfo")]
    [Index(nameof(UserId), nameof(DeleteTime), Name = "IX_UserId", IsUnique = true)]
    [Index(nameof(Email), nameof(DeleteTime), Name = "IX_Email", IsUnique = true)]
    [Index(nameof(PhoneNumber), nameof(DeleteTime), Name = "IX_PhoneNumber", IsUnique = true)]
    public partial class UserInfo : StringEntity
    {
        public UserInfo()
        {
            this.UserId = string.Empty;
            this.NickName = string.Empty;
            this.IsDisplayNickName = true;
            this.UploadFileId = string.Empty;
            this.Sex = (int)UserInfoSexEnum.男;
            this.BirthDate = DateTime.Now;
            this.PhoneNumber = string.Empty;
            this.Email = string.Empty;
            this.Description = string.Empty;
            this.Status = (int)UserInfoStatusEnum.公開;
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
        /// 暱稱
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 是否顯示暱稱
        /// </summary>
        public bool IsDisplayNickName { get; set; }

        /// <summary>
        /// 大頭貼檔案ID
        /// </summary>
        public string UploadFileId { get; set; }

        /// <summary>
        /// 性別
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// 手機號碼
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 電子郵件
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 個人描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 狀態
        /// </summary>
        public int Status { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
