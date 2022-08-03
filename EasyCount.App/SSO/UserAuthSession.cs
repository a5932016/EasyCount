namespace EasyCount.App.SSO
{
    [Serializable]
    public class UserAuthSession
    {
        public string UserId { get; set; }

        public string Token { get; set; }

        public string AppKey { get; set; }

        /// <summary>
        /// 使用者名稱
        /// </summary>
        public string Name { get; set; }

        public string IpAddress { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
