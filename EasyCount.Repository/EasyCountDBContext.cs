using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Infrastructure;
using Infrastructure.Utilities;
using EasyCount.Repository.Domain;

namespace EasyCount.Repository
{
    public partial class EasyCountDBContext : DbContext
    {
        private ILoggerFactory _LoggerFactory;
        private IHttpContextAccessor _httpContextAccessor;
        private IConfiguration _configuration;
        private IOptions<AppSetting> _appConfiguration;

        public EasyCountDBContext(DbContextOptions<EasyCountDBContext> options, ILoggerFactory loggerFactory,
            IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IOptions<AppSetting> appConfiguration)
            : base(options)
        {
            _LoggerFactory = loggerFactory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _appConfiguration = appConfiguration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging(true);  //允許打印參數
            optionsBuilder.UseLoggerFactory(_LoggerFactory);
            InitTenant(optionsBuilder);
            base.OnConfiguring(optionsBuilder);
        }

        //初始化信息，根據連線語法調整資料庫
        private void InitTenant(DbContextOptionsBuilder optionsBuilder)
        {
            var tenantId = _httpContextAccessor.GetTenantId();
            string connect = _configuration.GetConnectionString(tenantId);
            if (string.IsNullOrEmpty(connect))
            {
                throw new Exception($"未能找到{tenantId}對應的連接字串訊息");
            }

            //這個地方如果用IOption，在單元測試的時候會獲取不到AppSetting的值
            var dbtypes = _configuration.GetSection("AppSetting:DbTypes").GetChildren()
                .ToDictionary(x => x.Key, x => x.Value);

            var dbType = dbtypes[tenantId];
            if (dbType == Define.DBTYPE_MYSQL)  //mysql
            {
                optionsBuilder.UseMySql(connect, new MySqlServerVersion(new Version(Define.MYSQL_VERSION)));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public virtual DbSet<Application> Applications { get; set; }

        public virtual DbSet<Permission> Permissions { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<RolePermission> RolePermissions { get; set; }

        public virtual DbSet<SysLog> SysLogs { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<UserAccount> UserAccounts { get; set; }

        public virtual DbSet<UserDevice> UserDevices { get; set; }

        public virtual DbSet<UserInfo> UserInfos { get; set; }

        public virtual DbSet<UserRole> UserRoles { get; set; }

        public virtual DbSet<Counter> Counters { get; set; }
    }
}
