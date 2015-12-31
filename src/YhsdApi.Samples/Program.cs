using System;

namespace YhsdApi.Samples
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // 配置应用key和secret
            Configuration.AppKey = "790a310b972540de86b5c4817f04f459";
            Configuration.AppSecret = "4efdf06458ab4dd09d3972b83de7cd52";

            // 获取授权token
            var auth = new Auth();
            var token = auth.GetPrivateAppToken();

            // 调用API方法
            var api = new Api(token);
            Console.WriteLine(api.Get("customers"));
            Console.Read();
        }
    }
}