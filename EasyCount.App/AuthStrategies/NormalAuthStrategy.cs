using EasyCount.App.Base;
using EasyCount.App.Interface;
using EasyCount.Repository;
using EasyCount.Repository.Domain;
using EasyCount.Repository.Interface;

namespace EasyCount.App.AuthStrategies
{
    /// <summary>
    /// 普通用戶授權策略
    /// </summary>
    public class NormalAuthStrategy : BaseStringApp<User, EasyCountDBContext>, IAuthStrategy
    {
        protected UserInfo _userInfo;
        private List<string> _userRoleIds;    //用戶角色ID
        private List<string> _userPermissions;
        private DbExtension _dbExtension;

        //用戶角色
        public NormalAuthStrategy(IUnitWork<EasyCountDBContext> unitWork, IRepository<User, EasyCountDBContext> repository,
            DbExtension dbExtension) : base(unitWork, repository, null)
        {
            _dbExtension = dbExtension;
        }

        public List<Role> Roles
        {
            get { return UnitWork.Find<Role>(u => _userRoleIds.Contains(u.Id)).ToList(); }
        }

        public List<Permission> Permissions
        {
            get { return UnitWork.Find<Permission>(u => _userPermissions.Contains(u.Id)).ToList(); }
        }

        public UserInfo UserInfo
        {
            get { return _userInfo; }
            set
            {
                _userInfo = value;
                var roles = UnitWork.Find<UserRole>(u => u.UserId == _userInfo.Id && !u.DeleteTime.HasValue).ToList();
                var permissions = UnitWork.Find<RolePermission>(u => roles.Select(x => x.RoleId).Contains(u.RoleId) && !u.DeleteTime.HasValue).ToList();
                _userRoleIds = roles.Select(u => u.RoleId).ToList();
                _userPermissions = permissions.Select(x => x.PermissionId).ToList();
            }
        }
    }
}
