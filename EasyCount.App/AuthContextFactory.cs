using EasyCount.App.AuthStrategies;
using EasyCount.App.Interface;
using EasyCount.Repository;
using EasyCount.Repository.Domain;
using EasyCount.Repository.Interface;

namespace EasyCount.App
{
    /// <summary>
    ///  加載使用者所有可使用資源、權限
    /// </summary>
    public class AuthContextFactory
    {
        private SystemAuthStrategy _systemAuth;
        private NormalAuthStrategy _normalAuthStrategy;
        private readonly IUnitWork<EasyCountDBContext> _unitWork;

        public AuthContextFactory(SystemAuthStrategy sysStrategy
            , NormalAuthStrategy normalAuthStrategy
            , IUnitWork<EasyCountDBContext> unitWork)
        {
            _systemAuth = sysStrategy;
            _normalAuthStrategy = normalAuthStrategy;
            _unitWork = unitWork;
        }

        public AuthStrategyContext GetAuthStrategyContext(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return null;

            IAuthStrategy service = null;

            service = _normalAuthStrategy;
            service.UserInfo = _unitWork.FirstOrDefault<UserInfo>(u => u.Id == userId);

            return new AuthStrategyContext(service);
        }
    }
}
