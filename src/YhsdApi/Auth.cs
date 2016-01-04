using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using YhsdApi.Exceptions;

namespace YhsdApi
{
    /// <summary>
    /// 友好速搭平台授权类。
    /// </summary>
    public class Auth
    {
        /// <summary>
        /// 获取开放应用授权地址。
        /// </summary>
        /// <param name="redirectUrl"></param>
        /// <param name="shopKey"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public string GetPublicAppAuthorizeUrl(string redirectUrl, string shopKey, string state)
        {
            if (string.IsNullOrEmpty(Configuration.AppKey)) throw new MissingAppKeyException();
            if (string.IsNullOrEmpty(Configuration.AppSecret)) throw new MissingAppSecretException();

            var queryString = "?response_type=code";
            queryString += $"&client_id={Configuration.AppKey}";
            queryString += $"&shop_key={shopKey}";
            queryString += $"&scope={Configuration.Scope}";
            queryString += $"&redirect_uri={redirectUrl}";
            if (!string.IsNullOrEmpty(state)) queryString += $"&state={state}";

            return Configuration.AuthUrl + queryString;
        }

        /// <summary>
        /// 获取私有应用token。
        /// </summary>
        /// <returns>token。</returns>
        public string GetPrivateAppToken()
        {
            var client = new RestClient(Configuration.TokenUrl);
            client.AddDefaultHeader("content_type", "application/x-www-form-urlencoded");
            client.Authenticator = new HttpBasicAuthenticator(Configuration.AppKey, Configuration.AppSecret);
            var request = new RestRequest(Method.POST);
            request.AddParameter("grant_type", "client_credentials");
            var response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(response.Content);

            var token = JsonConvert.DeserializeObject<AccessToken>(response.Content).token;
            return token;
        }


        /// <summary>
        /// 获取公有应用token。
        /// </summary>
        /// <param name="redirectUrl">重定向地址。</param>
        /// <param name="code">可在请求中获取。</param>
        /// <returns>token.</returns>
        public string GetPublicAppToken(string redirectUrl, string code)
        {
            var client = new RestClient(Configuration.TokenUrl);
            client.AddDefaultHeader("content_type", "application/x-www-form-urlencoded");
            client.Authenticator = new HttpBasicAuthenticator(Configuration.AppKey, Configuration.AppSecret);
            var request = new RestRequest(Method.POST);
            request.AddParameter("grant_type", "authorization_code");
            request.AddParameter("code", code);
            request.AddParameter("client_id", Configuration.AppKey);
            request.AddParameter("redirect_url", redirectUrl);

            var response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var token = JsonConvert.DeserializeObject<AccessToken>(response.Content).token;
                return token;
            }

            return null;
        }

        /// <summary>
        /// 第三方接入支持。
        /// </summary>
        /// <param name="json">待加密数据。</param>
        /// <param name="Key">密钥。</param>
        /// <returns></returns>
        public string ThirdAppAesEncrypt(string json, string Key)
        {
            SymmetricAlgorithm rijndael = Rijndael.Create();

            var keyBytes = Encoding.UTF8.GetBytes(Key);
            rijndael.Key = keyBytes.Take(16).ToArray();
            rijndael.IV = keyBytes.Skip(16).Take(16).ToArray();
            byte[] output;
            using (var ms = new MemoryStream())
            {
                using (var stream = new CryptoStream(ms, rijndael.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    var input = Encoding.UTF8.GetBytes(json);
                    stream.Write(input, 0, input.Length);
                    stream.FlushFinalBlock();
                    output = ms.ToArray();
                    stream.Close();
                    ms.Close();
                }
            }

            return Convert.ToBase64String(output).Replace("+", "-").Replace("/", "_");
        }

        /// <summary>
        /// 验证HMAC。
        /// </summary>
        /// <param name="secret"></param>
        /// <param name="param">所有请求参数转换成字典。</param>
        /// <returns></returns>
        public bool VerifyHmac(string secret, Dictionary<string, string> param)
        {
            if (string.IsNullOrEmpty(secret)) throw new ArgumentNullException(nameof(secret));

            var hmac = param["hmac"];
            param.Remove("hmac");
            var keyValues = param.OrderBy(a => a.Key);
            var message = string.Join("&", keyValues.Select(a => a.Key + "=" + a.Value));
            var encoder = Encoding.GetEncoding("utf-8");
            var provider = new HMACSHA256(encoder.GetBytes(secret));
            var bytes = provider.ComputeHash(encoder.GetBytes(message));
            var computed = string.Join("", bytes.ToList().Select(a => a.ToString("x2")));

            return hmac == computed;
        }


        /// <summary>
        /// 验证webhook通知。
        /// </summary>
        /// <param name="token">访问令牌。</param>
        /// <param name="data">通知数据。</param>
        /// <param name="hmac">通知数据头部的hmac。</param>
        /// <returns></returns>
        public bool VerifyWebhook(string token, string data, string hmac)
        {
            if (string.IsNullOrEmpty(token)) throw new ArgumentNullException(nameof(token));
            if (string.IsNullOrEmpty(data)) throw new ArgumentNullException(nameof(data));
            if (string.IsNullOrEmpty(hmac)) throw new ArgumentNullException(nameof(hmac));

            var encoder = Encoding.GetEncoding("utf-8");
            var provider = new HMACSHA256(encoder.GetBytes(token));
            var bytes = provider.ComputeHash(encoder.GetBytes(data));
            var computed = Convert.ToBase64String(bytes);
            Console.WriteLine(computed);

            return computed == hmac;
        }

        internal class AccessToken
        {
            public string token { get; set; }
        }
    }
}