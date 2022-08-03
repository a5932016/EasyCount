namespace Infrastructure.Exceptions
{
    public enum ExceptionCode
    {
        ID不可為空 = 1,
        沒有權限 = 2,
        TOKEN過期 = 3,
        ENUM轉換失敗 = 4,
        第三方登入失敗 = 5,
        無NameIdentifier = 6,
        超過限制數量 = 7,
        已經沒人了 = 8,
        已經完成 = 9,
        JSON格式錯誤 = 10,
        查無資料 = 11,

        未註明錯誤 = 99,
        加載成功 = 200,
    }
}