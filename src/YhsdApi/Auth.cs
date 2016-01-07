using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace YhsdApi
{
    /// <summary>
    /// 友好速搭平台授权类。
    /// </summary>
    public class Auth
    {
        /// <summary>
        /// 第三方接入支持。
        /// </summary>
        /// <param name="json">待加密数据。</param>
        /// <param name="Key">密钥。</param>
        /// <returns></returns>
        public static string ThirdAppAesEncrypt(string json, string Key)
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
        public static bool VerifyHmac(string secret, Dictionary<string, string> param)
        {
            if (string.IsNullOrEmpty(secret)) throw new ArgumentNullException(nameof(secret));

            var hmac = param["hmac"];
            param.Remove("hmac");
            var keyValues = param.OrderBy(a => a.Key);
            var message = string.Join("&", keyValues.Select(a => a.Key + "=" + a.Value));
            var encoder = Encoding.UTF8;
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
        public static bool VerifyWebhook(string token, string data, string hmac)
        {
            if (string.IsNullOrEmpty(token)) throw new ArgumentNullException(nameof(token));
            if (string.IsNullOrEmpty(data)) throw new ArgumentNullException(nameof(data));
            if (string.IsNullOrEmpty(hmac)) throw new ArgumentNullException(nameof(hmac));

            var encoder = Encoding.UTF8;
            var provider = new HMACSHA256(encoder.GetBytes(token));
            var bytes = provider.ComputeHash(encoder.GetBytes(data));
            var computed = Convert.ToBase64String(bytes);

            return computed == hmac;
        }

        internal class AccessToken
        {
            public string token { get; set; }
        }
    }
}