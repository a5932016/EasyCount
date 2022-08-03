namespace EasyCount.App.Base
{
    /// <summary>
    /// table的返回資料
    /// </summary>
    public class TableData
    {
        /// <summary>
        /// 狀態碼
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 操作訊息
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 總計筆數
        /// </summary>
        public int count { get; set; }

        /// <summary>
        /// 資料內容
        /// </summary>
        public dynamic data { get; set; }

        public TableData()
        {
            code = 200;
            msg = "加載成功";
        }
    }
}
