using EasyCount.App.AuthStrategies;
using EasyCount.App.SSO;

namespace EasyCount.App.Interface
{
    public interface IAuth
    {
        /// <summary>
        /// 檢驗token是否有效
        /// </summary>
        /// <param name="token">token</param>
        /// <param name="otherInfo"></param>
        /// <returns></returns>
        bool CheckLogin(string token = "", string otherInfo = "");

        AuthStrategyContext GetCurrentUser();

        string GetUserName(string otherInfo = "");

        LoginResult Login(PassportLoginRequest request);

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        bool Logout();
    }
}
