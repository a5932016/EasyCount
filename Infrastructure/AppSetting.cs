namespace Infrastructure
{
    /// <summary>
    /// 配置項
    /// </summary>
    public class AppSetting
    {
        public AppSetting()
        {
            SSOPassport = "http://localhost:52789";
            Version = "";
            UploadPath = "";
            IdentityServerUrl = "";
        }

        /// <summary>
        /// SSO地址
        /// </summary>
        public string SSOPassport { get; set; }

        /// <summary>
        /// 版本訊息
        /// 如果為demo，則屏蔽Post請求
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 數據庫類型 SqlServer、MySql
        /// </summary>
        public Dictionary<string, string> DbTypes { get; set; }

        /// <summary>
        /// 附件上傳路徑
        /// </summary>
        public string UploadPath { get; set; }

        /// <summary>
        /// identity授權的地址
        /// </summary>
        public string IdentityServerUrl { get; set; }

        /// <summary>
        /// Redis服務器配置
        /// </summary>
        public string RedisConf { get; set; }

        /// <summary>
        /// 是否是Identity授權方式
        /// </summary>
        public bool IsIdentityAuth => !string.IsNullOrEmpty(IdentityServerUrl);
    }
}
