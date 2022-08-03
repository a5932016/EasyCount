namespace EasyCount.App.SSO
{
    public class PassportLoginRequest
    {
        public string Account { get; set; }

        public string Password { get; set; }

        /// <summary>
        /// 應用的AppSecrect，目前未啟用。如果需要判定請根據註釋調整LoginParse.Do方法
        /// </summary>
        /// <example>openauth</example>
        public string AppKey { get; set; }

        public void Trim()
        {
            if (string.IsNullOrEmpty(Account))
            {
                throw new Exception("用戶名不可為空");
            }

            if (string.IsNullOrEmpty(Password))
            {
                throw new Exception("密碼不可為空");
            }

            Account = Account.Trim();
            Password = Password.Trim();

            if (!string.IsNullOrEmpty(AppKey)) AppKey = AppKey.Trim();
        }
    }
}
