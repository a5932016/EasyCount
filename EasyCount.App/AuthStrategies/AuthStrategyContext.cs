using EasyCount.App.Interface;
using EasyCount.Repository.Domain;

namespace EasyCount.App.AuthStrategies
{
    /// <summary>
    /// 授權策略
    /// </summary>
    public class AuthStrategyContext
    {
        private readonly IAuthStrategy _strategy;

        public AuthStrategyContext(IAuthStrategy strategy)
        {
            this._strategy = strategy;
        }

        public UserInfo User
        {
            get { return _strategy.UserInfo; }
        }

        public List<Role> Roles
        {
            get { return _strategy.Roles; }
        }

        public List<Permission> Permissions
        {
            get { return _strategy.Permissions; }
        }
    }
}
