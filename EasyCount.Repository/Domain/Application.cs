using EasyCount.Repository.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyCount.Repository.Domain
{
    /// <summary>
    /// 應用表
    /// </summary>
    [Table("Application")]
    public partial class Application : StringEntity
    {
        public Application()
        {
            this.Name = string.Empty;
            this.AppSecret = string.Empty;
            this.Description = string.Empty;
            this.Icon = string.Empty;
            this.Domain = string.Empty;
        }

        /// <summary>
        /// 名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 密鑰
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// ICON
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool Disable { get; set; }

        /// <summary>
        /// 網域
        /// </summary>
        public string Domain { get; set; }
    }
}
