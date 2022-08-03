using EasyCount.Repository.Core;
using EasyCount.Repository.Enum;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyCount.Repository.Domain
{
    /// <summary>
    /// 權限表
    /// </summary>
    [Table("Permission")]
    [Index(nameof(Name), nameof(DeleteTime), Name = "IX_PermissionName", IsUnique = true)]
    public partial class Permission : StringEntity
    {
        public Permission()
        {
            this.Name = string.Empty;
            this.Url = string.Empty;
            this.IconName = string.Empty;
            this.Status = (int)PermissionStatusEnum.啟用;
            this.Level = 0;
            this.ParentId = string.Empty;
            this.ParentName = string.Empty;
            this.SortNo = 1;
        }

        /// <summary>
        /// 名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 主頁面URL
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 是否自動展開
        /// </summary>
        public bool IsAutoExpand { get; set; }

        /// <summary>
        /// 是否系統權限
        /// </summary>
        public bool IsSys { get; set; }

        /// <summary>
        /// ICON名稱
        /// </summary>
        public string IconName { get; set; }

        /// <summary>
        /// 狀態
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 層級
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 父節點ID
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 父節點名稱
        /// </summary>
        public string ParentName { get; set; }

        /// <summary>
        /// 排序號
        /// </summary>
        public int SortNo { get; set; }
    }
}
