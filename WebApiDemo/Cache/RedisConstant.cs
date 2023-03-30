namespace WebApiDemo.Cache
{
    public static class RedisConstant
    {
        /// <summary>
        /// 程序保存redis前缀
        /// </summary>
        public static readonly string MAIN_KEY = "mywebapi:";

        /// <summary>
        /// 登录失败的账号次数
        /// </summary>
        public static readonly string LIMIT_LOGIN_ACCOUNT_KEY = MAIN_KEY + "limit_login:account:";


        /// <summary>
        /// 定时任务中上次同步时间
        /// </summary>
        public static readonly string LAST_SYNC_CDUI_TIME = MAIN_KEY + "last_sync_time:sync_cdui_time";
    }
}
