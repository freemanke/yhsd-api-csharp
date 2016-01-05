# yhsd-api-csharp

友好速搭应用开发 CSharp SDK

### 安装方法

使用 Package Manager Console 安装

    Install-Package YhsdApi

### 使用方法

	using System;

	namespace YhsdApi.Samples
	{
	    internal class Program
	    {
	        private static string appKey = "790a310b972540de86b5c4817f04f459";
	        private static string appSecret = "4efdf06458ab4dd09d3972b83de7cd52";
	        private static string appRedirectUrl = "http://redirecturl.com";
	        private static string code = "d2556efb2ca3425ea48234090cda6c8c";
	
	        private static void Main(string[] args)
	        {
	            PrivateAppSample();
	            PublicAppSample();
	            Console.Read();
	        }
	
	        /// <summary>
	        /// 私有应用API使用方法。
	        /// </summary>
	        private static void PrivateAppSample()
	        {
	            var auth = new PrivateAppAuth(appKey, appSecret);
	            var token = auth.GetToken();
	            var api = new Api(token);
	            var response = api.Get("customers");
	            Console.WriteLine(response.Content);
	        }
	
	        /// <summary>
	        /// 公有应用API使用方法。
	        /// </summary>
	        private static void PublicAppSample()
	        {
	            var auth = new PublicAppAuth(appKey, appSecret, appRedirectUrl, "read_basic");
	            var token = auth.GetToken(code);
	            Console.WriteLine(token);
	            var api = new Api(token);
	            var response = api.Get("customers");
	            Console.WriteLine(response.Content);
	        }
	    }
	}