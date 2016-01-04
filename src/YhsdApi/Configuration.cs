namespace YhsdApi
{
    /// <summary>
    /// API配置信息。
    /// </summary>
    internal static class Configuration
    {
        static Configuration()
        {
            AppKey = "";
            AppSecret = "";
            AppRedirectUrl = "";
            TokenUrl = "https://apps.youhaosuda.com/oauth2/token/";
            ApiUrl = "https://api.youhaosuda.com/";
            ApiVersion = "v1/";
            CallLimitProtect = true;

            Scope = "";
        }

        public static string AppKey { get; set; }
        public static string AppSecret { get; set; }
        public static string AppRedirectUrl { get; set; }
        public static string TokenUrl { get; set; }
        public static string ApiUrl { get; set; }
        public static string ApiVersion { get; set; }
        public static string Scope { get; set; }
        public static bool CallLimitProtect { get; set; }
    }
}