namespace YhsdApi
{
    /// <summary>
    /// API配置信息。
    /// </summary>
    public static class Configuration
    {
        private static string appRedirectUrl;
        private static string tokenUrl;
        private static string authUrl;
        private static string apiUrl;
        private static string apiVersion;

        static Configuration()
        {
            AppKey = "";
            AppSecret = "";
            appRedirectUrl = "";
            tokenUrl = "https://apps.youhaosuda.com/oauth2/token";
            apiUrl = "https://api.youhaosuda.com";
            authUrl = "https://apps.youhaosuda.com/oauth2/authorize";
            apiVersion = "v1";
            CallLimitProtect = true;

            Scope = "";
        }

        public static string AppKey { get; set; }
        public static string AppSecret { get; set; }

        /// <summary>
        /// 重定向地址，公有应用使用。
        /// </summary>
        public static string AppRedirectUrl
        {
            get { return appRedirectUrl; }
            set { if (!string.IsNullOrEmpty(value)) appRedirectUrl = value.TrimEnd('/'); }
        }

        /// <summary>
        /// 获取token地址，一般无需修改。
        /// 默认值为：https://apps.youhaosuda.com/oauth2/token。
        /// </summary>
        public static string TokenUrl
        {
            get { return tokenUrl; }
            set { if (!string.IsNullOrEmpty(value)) tokenUrl = value.TrimEnd('/'); }
        }

        /// <summary>
        /// 获取授权地址，一般无需修改。
        /// 默认值为：https://apps.youhaosuda.com/oauth2/authorize。
        /// </summary>
        public static string AuthUrl
        {
            get { return authUrl; }
            set { if (!string.IsNullOrEmpty(value)) authUrl = value.TrimEnd('/'); }
        }

        /// <summary>
        /// API地址，一般无需修改。
        /// 默认值为：https://api.youhaosuda.com。
        /// </summary>
        public static string ApiUrl
        {
            get { return apiUrl; }
            set { if (!string.IsNullOrEmpty(value)) apiUrl = value.TrimEnd('/'); }
        }

        /// <summary>
        /// API版本号，一般无需修改，当前为：v1。
        /// </summary>
        public static string ApiVersion
        {
            get { return apiVersion; }
            set { if (!string.IsNullOrEmpty(value)) apiVersion = value.Trim('/'); }
        }

        public static string Scope { get; set; }
        public static bool CallLimitProtect { get; set; }
    }
}