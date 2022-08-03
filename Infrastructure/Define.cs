namespace Infrastructure
{
    public static class Define
    {
        public const string DBTYPE_SQLSERVER = "SqlServer";    //sql server

        public const string DBTYPE_MYSQL = "MySql";    //mysql
        public const string MYSQL_VERSION = "8.0.28";

        public const string DBTYPE_ORACLE = "Oracle";    //oracle

        public const int INVALID_TOKEN = 50014;     //token无效

        public const string TOKEN_NAME = "X-Token";
        public const string TENANT_ID = "tenantId";

        public const string SYSTEM_USERNAME = "System";
        public const string SYSTEM_USERPWD = "123456";

        public const string JOBMAPKEY = "OpenJob";

        /// <summary>
        /// 快取存活天數
        /// </summary>
        public const int CACHE_DATE = 1;
    }
}