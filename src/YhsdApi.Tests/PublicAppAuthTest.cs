using System;
using System.Linq;
using NUnit.Framework;

namespace YhsdApi.Tests
{
    public class PublicAppAuthTest
    {
        private const string appKey = "38b76c587b4340bba166e36f35094f1f";
        private const string appSecret = "83c5a436ae1e410e88ed8bcea502fdec";
        private PublicAppAuth auth;
        private const string scope = "read_basic,write_basic";

        [TestFixtureSetUp]
        public void SetUp()
        {
            auth = new PublicAppAuth(appKey, appSecret, "http://baidu.com", scope);
        }

        [Test]
        public void GetAuthorizeUrl()
        {
            var authorizeUrl = auth.GetAuthorizeUrl("a5547f8186976bb04d132dd56cf2887a", "");
            Console.WriteLine(authorizeUrl);
            Assert.NotNull(authorizeUrl);
        }


        [Test]
        public void GetToken()
        {
            var token = auth.GetToken("f08ea171d0db4b1f9b60dde5084f818f");
            Console.WriteLine(token);
            Assert.NotNull(token);
        }
    }
}