using System;
using System.Net;
using Newtonsoft.Json;
using RestSharp;
using YhsdApi.Exceptions;

namespace YhsdApi
{
    /// <summary>
    /// 公共应用授权类。
    /// </summary>
    public class PublicAppAuth : Auth
    {
        /// <summary>
        /// 构造方法。
        /// </summary>
        /// <param name="appKey">插件/应用的appKey,可在合作伙伴后台获取。</param>
        /// <param name="appSecret">插件/应用的appSecret,可在合作伙伴后台获取。</param>
        /// <param name="appRedirectUrl"></param>
        /// <param name="scope">必填，</param>
        public PublicAppAuth(string appKey, string appSecret, string appRedirectUrl, string scope)
        {
            if (string.IsNullOrEmpty(appKey)) throw new MissingAppKeyException();
            if (string.IsNullOrEmpty(appSecret)) throw new MissingAppSecretException();
            if (string.IsNullOrEmpty(appRedirectUrl)) throw new ArgumentNullException(nameof(appRedirectUrl));
            if (string.IsNullOrEmpty(scope)) throw new ArgumentNullException(nameof(scope));

            Configuration.AppKey = appKey;
            Configuration.AppSecret = appSecret;
            Configuration.AppRedirectUrl = appRedirectUrl;
            Configuration.Scope = scope.Replace("\r", "").Replace("\n", "");
        }

        /// <summary>
        /// 获取开放应用授权地址。
        /// </summary>
        /// <param name="shopKey">shop key,可在请求中获取。</param>
        /// <param name="state">要携带的参数。</param>
        /// <returns></returns>
        public string GetAuthorizeUrl(string shopKey, string state)
        {
            if (string.IsNullOrEmpty(Configuration.AppKey)) throw new MissingAppKeyException();
            if (string.IsNullOrEmpty(Configuration.AppSecret)) throw new MissingAppSecretException();

            var queryString = "?response_type=code";
            queryString += $"&client_id={Configuration.AppKey}";
            queryString += $"&shop_key={shopKey}";
            queryString += $"&scope={Configuration.Scope}";
            queryString += $"&redirect_uri={Configuration.AppRedirectUrl}";
            if (!string.IsNullOrEmpty(state)) queryString += $"&state={state}";

            return Configuration.AuthUrl + queryString;
        }

        /// <summary>
        /// 获取公有应用token。
        /// </summary>
        /// <param name="code">可在请求中获取。</param>
        /// <returns>token.</returns>
        public string GetToken(string code)
        {
            var client = new RestClient(Configuration.TokenUrl);
            client.AddDefaultHeader("Content-Type", "application/x-www-form-urlencoded");
            var request = new RestRequest(Method.POST);
            request.AddParameter("grant_type", "authorization_code");
            request.AddParameter("code", code);
            request.AddParameter("client_id", Configuration.AppKey);
            request.AddParameter("redirect_uri", Configuration.AppRedirectUrl);
            var response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var token = JsonConvert.DeserializeObject<AccessToken>(response.Content).token;
                return token;
            }

            return null;
        }
    }
}