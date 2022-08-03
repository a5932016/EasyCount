namespace EasyCount.App.Base
{
    /// <summary>
    /// 返回確定類型的表格數據，可以為swagger提供的精準的註釋
    /// </summary>
    public class TableResp<T>
    {
        /// <summary>
        /// 狀態碼
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 操作消息
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 總計比數
        /// </summary>
        public int count { get; set; }

        /// <summary>
        /// 數據內容
        /// </summary>
        public List<T> data { get; set; }

        ///// <summary>
        /////  返回的列表头信息
        ///// </summary>
        //public List<KeyDescription> columnHeaders { get; set; }

        public TableResp()
        {
            code = 200;
            msg = "加載成功";
            //columnHeaders = new List<KeyDescription>();
        }
    }
}
