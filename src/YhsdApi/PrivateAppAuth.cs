using System;
using System.Net;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using YhsdApi.Exceptions;

namespace YhsdApi
{
    /// <summary>
    /// 私有应用授权类。
    /// </summary>
    public class PrivateAppAuth : Auth
    {
        /// <summary>
        /// 构造方法。
        /// </summary>
        /// <param name="appKey">插件/应用的appKey,可在合作伙伴后台获取。</param>
        /// <param name="appSecret">插件/应用的appSecret,可在合作伙伴后台获取。</param>
        public PrivateAppAuth(string appKey, string appSecret)
        {
            if (string.IsNullOrEmpty(appKey)) throw new MissingAppKeyException();
            if (string.IsNullOrEmpty(appSecret)) throw new MissingAppSecretException();

            Configuration.AppKey = appKey;
            Configuration.AppSecret = appSecret;
        }

        /// <summary>
        /// 获取应用token。
        /// </summary>
        /// <returns>token。</returns>
        public string GetToken()
        {
            var client = new RestClient(Configuration.TokenUrl);
            client.AddDefaultHeader("Content-Type", "application/x-www-form-urlencoded");
            client.Authenticator = new HttpBasicAuthenticator(Configuration.AppKey, Configuration.AppSecret);
            var request = new RestRequest(Method.POST);
            request.AddParameter("grant_type", "client_credentials");
            var response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(response.Content);

            var token = JsonConvert.DeserializeObject<AccessToken>(response.Content).token;
            return token;
        }
    }
}