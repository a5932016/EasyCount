using EasyCount.App.Base;
using EasyCount.App.Interface;
using EasyCount.Repository;
using EasyCount.Repository.Domain;
using EasyCount.Repository.Interface;

namespace EasyCount.App.AuthStrategies
{
    /// <summary>
    /// 超級管理者權限
    /// </summary>
    public class SystemAuthStrategy : BaseStringApp<User, EasyCountDBContext>, IAuthStrategy
    {
        protected UserInfo _userInfo;
        private DbExtension _dbExtension;

        public SystemAuthStrategy(IUnitWork<EasyCountDBContext> unitWork, IRepository<User, EasyCountDBContext> repository,
            DbExtension dbExtension) : base(unitWork, repository, null)
        {
            _dbExtension = dbExtension;
            _userInfo = new UserInfo
            {
                Id = Guid.Empty.ToString()
            };
        }

        public List<Role> Roles
        {
            get { return UnitWork.Find<Role>(null).ToList(); }
        }

        public List<Permission> Permissions
        {
            get { return UnitWork.Find<Permission>(null).ToList(); }
        }

        public UserInfo UserInfo
        {
            get { return _userInfo; }
            set   //禁止外部設定
            {
                throw new Exception("超級管理員，禁止設置用戶");
            }
        }
    }
}
