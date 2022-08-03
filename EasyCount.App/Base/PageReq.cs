namespace EasyCount.App.Base
{
    public class PageReq
    {
        /// <summary>
        /// 頁碼
        /// </summary>
        /// <example>1</example>
        public int page { get; set; }

        /// <summary>
        /// 每頁筆數
        /// </summary>
        /// <example>10</example>
        public int limit { get; set; }

        public string key { get; set; }

        public PageReq()
        {
            page = 1;
            limit = 10;
        }
    }
}
