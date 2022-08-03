using EasyCount.App.Apps;
using EasyCount.App.Apps.SysLogs;
using EasyCount.App.AuthStrategies;
using EasyCount.App.Interface;
using EasyCount.Repository.Domain;
using EasyCount.Repository.Enum;
using Infrastructure;
using Infrastructure.Cache;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace EasyCount.App.SSO
{
    /// <summary>
    /// 使用本地登陸。這個註冊IAuth時，只需要EasyCount.Mvc一個項目即可，無須webapi的支持
    /// </summary>
    public class LocalAuth : IAuth
    {
        private IHttpContextAccessor _httpContextAccessor;
        private IOptions<AppSetting> _appConfiguration;
        private SysLogApp _logApp;

        private AuthContextFactory _app;
        private LoginParse _loginParse;
        private ICacheContext _cacheContext;

        public LocalAuth(IHttpContextAccessor httpContextAccessor
            , AuthContextFactory app
            , LoginParse loginParse
            , ICacheContext cacheContext, IOptions<AppSetting> appConfiguration, SysLogApp logApp)
        {
            _httpContextAccessor = httpContextAccessor;
            _app = app;
            _loginParse = loginParse;
            _cacheContext = cacheContext;
            _appConfiguration = appConfiguration;
            _logApp = logApp;
        }

        /// <summary>
        /// 取得TOKEN
        /// </summary>
        /// <returns></returns>
        private string GetToken()
        {
            string token = _httpContextAccessor.HttpContext.Request.Query[Define.TOKEN_NAME];
            if (!String.IsNullOrEmpty(token)) return token;

            token = _httpContextAccessor.HttpContext.Request.Headers[Define.TOKEN_NAME];
            if (!String.IsNullOrEmpty(token)) return token;

            var cookie = _httpContextAccessor.HttpContext.Request.Cookies[Define.TOKEN_NAME];

            return cookie ?? String.Empty;
        }

        /// <summary>
        /// 檢驗token是否有效
        /// </summary>
        /// <param name="token"></param>
        /// <param name="otherInfo"></param>
        /// <returns></returns>
        public bool CheckLogin(string token = "", string otherInfo = "")
        {
            if (string.IsNullOrEmpty(token))
            {
                token = GetToken();
            }

            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            try
            {
                var result = _cacheContext.Get<UserAuthSession>(token) != null;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 獲取當前登陸的用戶訊息
        /// <para>通過URL中的Token參數或Cookie中的Token</para>
        /// </summary>
        /// <param name="account">The account.</param>
        /// <returns>LoginUserVM.</returns>
        public AuthStrategyContext GetCurrentUser()
        {
            AuthStrategyContext context = null;

            var user = _cacheContext.Get<UserAuthSession>(GetToken());
            if (user != null)
            {
                context = _app.GetAuthStrategyContext(user.UserId);
            }

            return context;
        }

        /// <summary>
        /// 獲取當前登陸的用戶名
        /// <para>通過URL中的Token參數或Cookie中的Token</para>
        /// </summary>
        /// <param name="otherInfo">The account.</param>
        /// <returns>System.String.</returns>
        public string GetUserName(string otherInfo = "")
        {
            var user = _cacheContext.Get<UserAuthSession>(GetToken());
            if (user != null)
            {
                return user.Name;
            }

            return "";
        }

        public LoginResult Login(PassportLoginRequest request)
        {
            var result = _loginParse.Do(request);

            var log = new SysLog
            {
                Content = $"使用者登入，結果：{result.Message}",
                Result = result.Code == 200 ? (int)SysLogResultEnum.成功 : (int)SysLogResultEnum.失敗,
                CreateUserId = request.Account,
                CreateUserName = request.Account,
                CreateTime = DateTime.Now,
                TypeName = "登入日誌"
            };

            _logApp.Add(log);

            return result;
        }

        /// <summary>
        /// 登出
        /// </summary>
        public bool Logout()
        {
            var token = GetToken();
            if (String.IsNullOrEmpty(token)) return true;

            try
            {
                _cacheContext.Remove(token);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
