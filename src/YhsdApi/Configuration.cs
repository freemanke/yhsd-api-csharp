namespace YhsdApi
{
    /// <summary>
    /// API配置信息。
    /// </summary>
    public static class Configuration
    {
        static Configuration()
        {
            AppKey = "";
            AppSecret = "";
            AuthUrl = "https://apps.youhaosuda.com/oauth2/authorize/";
            TokenUrl = "https://apps.youhaosuda.com/oauth2/token/";
            ApiUrl = "https://api.youhaosuda.com/";
            ApiVersion = "v1/";
            Scope = "";
            CallLimitProtect = false;
        }

        public static string AppKey { get; set; }
        public static string AppSecret { get; set; }
        public static string AuthUrl { get; set; }
        public static string TokenUrl { get; set; }
        public static string ApiUrl { get; set; }
        public static string ApiVersion { get; set; }
        public static string Scope { get; set; }
        public static bool CallLimitProtect { get; set; }
    }
}