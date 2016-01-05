using System;
using NUnit.Framework;

namespace YhsdApi.Tests
{
    public class PublicAppAuthTest
    {
        private const string appKey = "38b76c587b4340bba166e36f35094f1f";
        private const string appSecret = "83c5a436ae1e410e88ed8bcea502fdec";
        private const string scope = "read_basic,write_basic";
        private PublicAppAuth auth;

        [TestFixtureSetUp]
        public void SetUp()
        {
            auth = new PublicAppAuth(appKey, appSecret, "http://baidu.com", scope);
        }

        [Test]
        public void GetToken()
        {
            // 使用安装地址：https://apps.youhaosuda.com/devel/ba0e4b052c0f42018520f8a32b43e15c
            // 在浏览器中操作，将该公共应用安装到你的店铺中，完成后浏览器导航到类似如下地址：
            // https://youhaosuda.com/callback?account_id=9903&hmac=d648d18bd34a4938c77566f0ccc0c5de9bb92943b3b4d67c43f9670ced5e477f&shop_key=ca3fa0e6971a54c0b5084f6f19b8521b&time_stamp=2016-01-05T03%3A56%3A19Z
            // 使用改地址中的shop_key获取授权地址
            var authorizeUrl = auth.GetAuthorizeUrl("a5547f8186976bb04d132dd56cf2887a", "");
            Console.WriteLine(authorizeUrl);
            Assert.NotNull(authorizeUrl);

            // 通过以上操作获取到如下授权地址：https://apps.youhaosuda.com/oauth2/authorize?response_type=code&client_id=38b76c587b4340bba166e36f35094f1f&shop_key=a5547f8186976bb04d132dd56cf2887a&scope=read_basic,write_basic&redirect_uri=http://baidu.com
            // 在浏览器中导航到该授权地址，并在页面上确认授权并安装，页面将导航到类似如下地址：
            // https://www.baidu.com/?account_id=9903&code=d2556efb2ca3425ea48234090cda6c8c&hmac=2496ffa1d31c5b51983853ba363d75ab64201c8ad372df4c4d6991b68526db1e&shop_key=a5547f8186976bb04d132dd56cf2887a&time_stamp=2016-01-05T03%3A58%3A29Z
            // 从该地址中取得code值，并获取token
            var code = "d2556efb2ca3425ea48234090cda6c8c";
            var token = auth.GetToken(code);
            Console.WriteLine(token);
            Assert.NotNull(token);
        }
    }
}