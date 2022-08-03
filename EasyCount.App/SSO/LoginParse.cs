using EasyCount.App.Apps.Applications;
using EasyCount.Repository;
using EasyCount.Repository.Domain;
using EasyCount.Repository.Enum;
using EasyCount.Repository.Interface;
using Infrastructure;
using Infrastructure.Cache;

namespace EasyCount.App.SSO
{
    public class LoginParse
    {
        //這個地方使用IRepository<User> 而不使用UserManagerApp是防止循環依賴
        public IRepository<User, EasyCountDBContext> _app;
        private ICacheContext _cacheContext;
        private ApplicationApp _appInfoService;

        public LoginParse(ApplicationApp infoService, ICacheContext cacheContext, IRepository<User, EasyCountDBContext> userApp)
        {
            _appInfoService = infoService;
            _cacheContext = cacheContext;
            _app = userApp;
        }

        public LoginResult Do(PassportLoginRequest model)
        {
            var result = new LoginResult();
            try
            {
                model.Trim();
                //todo:如果需要判定應用，可以取消該註釋
                // var appInfo = _appInfoService.GetByAppKey(model.AppKey);
                // if (appInfo == null)
                // {
                //     throw  new Exception("應用不存在");
                // }
                //獲取用戶信息

                // 執行雜湊後比對
                string password = model.Password;

                User user = _app.FirstOrDefault(p =>
                    !p.DeleteTime.HasValue &&
                    p.UserAccounts.Any(x =>
                    x.Account == model.Account &&
                    x.Password == password &&
                    x.Status == (int)UserAccountStatusEnum.啟用 &&
                    !x.DeleteTime.HasValue));

                if (user == null)
                {
                    throw new Exception("帳號或密碼錯誤");
                }

                if (user.Status != (int)UserStatusEnum.啟用)
                {
                    throw new Exception("帳號狀態異常，可能已經停用");
                }

                var currentSession = new UserAuthSession
                {
                    UserId = user.Id,
                    Name = user.Name,
                    Token = Guid.NewGuid().ToString(),
                    AppKey = model.AppKey,
                    CreateTime = DateTime.Now,
                    //IpAddress = HttpContext.Current.Request.UserHostAddress
                };

                //創建Session
                _cacheContext.Set(currentSession.Token, currentSession, DateTime.Now.AddDays(Define.CACHE_DATE));

                result.Code = 200;
                result.Token = currentSession.Token;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }

            return result;
        }
    }
}
