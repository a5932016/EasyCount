using System.ComponentModel;

namespace EasyCount.Repository.Core
{
    public class StringEntity : BaseEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        [Browsable(false)]
        public string Id { get; set; }

        /// <summary>
        /// 創建時間
        /// </summary>
        [Description("創建時間")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 創建人ID
        /// </summary>
        [Description("創建人ID")]
        public string CreateUserId { get; set; }

        /// <summary>
        /// 創建人姓名
        /// </summary>
        [Description("創建人姓名")]
        public string CreateUserName { get; set; }

        /// <summary>
        /// 編輯時間
        /// </summary>
        [Description("編輯時間")]
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 編輯人ID
        /// </summary>
        [Description("編輯人ID")]
        public string? UpdateUserId { get; set; }

        /// <summary>
        /// 編輯人姓名
        /// </summary>
        [Description("編輯人姓名")]
        public string? UpdateUserName { get; set; }

        /// <summary>
        /// 刪除時間
        /// </summary>
        [Description("刪除時間")]
        public DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 刪除人ID
        /// </summary>
        [Description("刪除人ID")]
        public string? DeleteUserId { get; set; }

        /// <summary>
        /// 刪除人姓名
        /// </summary>
        [Description("刪除人姓名")]
        public string? DeleteUserName { get; set; }

        /// <summary>
        /// 判斷主鍵是否為空，常用做判定操作是【添加】還是【編輯】
        /// </summary>
        /// <returns></returns>
        public override bool KeyIsNull()
        {
            return string.IsNullOrEmpty(Id);
        }

        /// <summary>
        /// 創建默認的主鍵值
        /// </summary>
        public override void GenerateDefaultKeyVal()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
