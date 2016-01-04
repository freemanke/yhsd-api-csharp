using NUnit.Framework;

namespace YhsdApi.Tests
{
    public class PublicAppAuthTest
    {
        private const string appKey = "790a310b972540de86b5c4817f04f459";
        private const string appSecret = "4efdf06458ab4dd09d3972b83de7cd52";
        private PublicAppAuth auth;

        [TestFixtureSetUp]
        public void SetUp()
        {
            auth = new PublicAppAuth(appKey, appSecret, "http://redirecturl.com", "read_basic");
        }

        [Test]
        public void GetAuthorizeUrl()
        {
            Assert.NotNull(auth.GetAuthorizeUrl("shop-key", ""));
        }

        [Test]
        public void GetToken()
        {
            Assert.NotNull(auth.GetToken("read_basic"));
        }
    }
}