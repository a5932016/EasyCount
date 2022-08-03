using EasyCount.Repository.Domain;

namespace EasyCount.App.Interface
{
    public interface IAuthStrategy
    {
        List<Role> Roles { get; }

        UserInfo UserInfo { get; set; }

        List<Permission> Permissions { get; }
    }
}
