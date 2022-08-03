using EasyCount.Repository.Core;
using EasyCount.Repository.Enum;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyCount.Repository.Domain
{
    [Table("Counter")]
    [Index(nameof(ViewId), nameof(DeleteTime), Name = "IX_ViewId", IsUnique = true)]
    public partial class Counter : StringEntity
    {
        public Counter()
        {
            this.ViewId = string.Empty;
            this.Title = string.Empty;
            this.Description = string.Empty;
            this.Count = 0;
            this.LimitCount = 0;
            this.Status = (int)CounterStatusEnum.啟用;
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

        public override void GenerateDefaultKeyVal()
        {
            this.Id = Guid.NewGuid().ToString();
            this.ViewId = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// 檢視ID
        /// </summary>
        public string ViewId { get; set; }

        /// <summary>
        /// 名稱
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 數量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 限制數量
        /// </summary>
        public int LimitCount { get; set; }

        /// <summary>
        /// 狀態
        /// </summary>
        public int Status { get; set; }
    }
}
