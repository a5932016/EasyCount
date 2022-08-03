using EasyCount.Repository.Core;
using EasyCount.Repository.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyCount.Repository.Domain
{
    /// <summary>
    /// 系統日誌表
    /// </summary>
    [Table("SysLog")]
    public partial class SysLog : StringEntity
    {
        public SysLog()
        {
            this.Content = string.Empty;
            this.TypeName = string.Empty;
            this.TypeId = string.Empty;
            this.Href = string.Empty;
            this.CreateTime = DateTime.Now;
            this.Ip = string.Empty;
            this.Application = string.Empty;
            this.Result = (int)SysLogResultEnum.成功;
        }

        /// <summary>
        /// 日誌內容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 分類名稱
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 分類ID
        /// </summary>
        public string TypeId { get; set; }

        /// <summary>
        /// 操作所屬地址
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// 操作者IP地址
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 操作結果
        /// </summary>
        public int Result { get; set; }

        /// <summary>
        /// 應用來源
        /// </summary>
        public string Application { get; set; }
    }
}
