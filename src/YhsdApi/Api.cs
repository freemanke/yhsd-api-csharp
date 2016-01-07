using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using RestSharp;
using YhsdApi.Exceptions;

namespace YhsdApi
{
    /// <summary>
    /// 友好速搭平台API。
    /// </summary>
    public class Api
    {
        private readonly RestClient client = new RestClient();
        private DateTime lastAt = DateTime.MinValue;
        private int limit;
        private string token;
        private int total;

        /// <summary>
        /// 构造方法。
        /// </summary>
        /// <param name="token">可以通过授权类获取。</param>
        public Api(string token)
        {
            this.token = token;
        }

        /// <summary>
        /// GET请求。
        /// </summary>
        /// <param name="path">相对路径。</param>
        /// <param name="param">请求参数字典。</param>
        /// <returns><see cref="IRestResponse"/>响应对象。</returns>
        public IRestResponse Get(string path, Dictionary<string, string> param = null)
        {
            if (string.IsNullOrEmpty(path)) throw new MissingUrlException();

            client.BaseUrl = new Uri(GetApiBaseUrl() + path.Trim('/') + GetQueryString(param));
            var request = new RestRequest(Method.GET);
            request.AddHeader("X-API-ACCESS-TOKEN", token);
            request.AddHeader("Content-Type", "application/json");

            BeginRequest();
            var response = client.Execute(request);
            AfterRequest(response.Headers);

            return response;
        }

        /// <summary>
        /// POST请求。
        /// </summary>
        /// <param name="path">相对路径。</param>
        /// <param name="body">提交的对象。</param>
        /// <returns><see cref="IRestResponse"/>响应对象。</returns>
        public IRestResponse Post(string path, object body)
        {
            if (string.IsNullOrEmpty(path)) throw new MissingUrlException();

            client.BaseUrl = new Uri(GetApiBaseUrl() + path.Trim('/'));
            var request = new RestRequest(Method.POST);
            request.AddHeader("X-API-ACCESS-TOKEN", token);
            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;
            if (body != null) request.AddBody(body);

            BeginRequest();
            var response = client.Execute(request);
            AfterRequest(response.Headers);

            return response;
        }

        /// <summary>
        /// PUT请求。
        /// </summary>
        /// <param name="path">相对路径。</param>
        /// <param name="body">提交的对象。</param>
        /// <returns><see cref="IRestResponse"/>响应对象。</returns>
        public IRestResponse Put(string path, object body)
        {
            if (string.IsNullOrEmpty(path)) throw new MissingUrlException();

            client.BaseUrl = new Uri(GetApiBaseUrl() + path.Trim('/'));
            var request = new RestRequest(Method.PUT);
            request.AddHeader("X-API-ACCESS-TOKEN", token);
            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;
            if (body != null) request.AddBody(body);

            BeginRequest();
            var response = client.Execute(request);
            AfterRequest(response.Headers);

            return response;
        }

        /// <summary>
        /// PUT请求。
        /// </summary>
        /// <param name="path">相对路径。</param>
        /// <returns><see cref="IRestResponse"/>响应对象。</returns>
        public IRestResponse Delete(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new MissingUrlException();

            client.BaseUrl = new Uri(GetApiBaseUrl() + path.Trim('/'));
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("X-API-ACCESS-TOKEN", token);

            BeginRequest();
            var response = client.Execute(request);
            AfterRequest(response.Headers);

            return response;
        }

        private static string GetApiBaseUrl() => Configuration.ApiUrl + "/" + Configuration.ApiVersion + "/";

        private void BeginRequest()
        {
            if (!Configuration.CallLimitProtect) return;

            if (lastAt != DateTime.MinValue)
                if ((DateTime.Now - lastAt) < TimeSpan.FromMilliseconds(1000) && total - limit <= 2)
                    Thread.Sleep(1000);
        }

        private void AfterRequest(IEnumerable<Parameter> headers)
        {
            if (!Configuration.CallLimitProtect) return;
            var x = headers?.FirstOrDefault(a => a.Name.ToLower() == "x-yhsd-shop-api-call-limit");
            if (x != null)
            {
                var call_limit = x.Value.ToString().Split('/');
                limit = int.Parse(call_limit[0]);
                total = int.Parse(call_limit[1]);
                lastAt = DateTime.Now;
            }
        }

        private static string GetQueryString(Dictionary<string, string> param)
        {
            var query = "";
            if (param != null) query += "?" + string.Join("&", param.Select(a => a.Key + "=" + a.Value));

            return query;
        }
    }
}