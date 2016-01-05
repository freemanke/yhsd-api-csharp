using NUnit.Framework;

namespace YhsdApi.Tests
{
    public class PrivateAppAuthTest
    {
        private const string appKey = "790a310b972540de86b5c4817f04f459";
        private const string appSecret = "4efdf06458ab4dd09d3972b83de7cd52";
        private PrivateAppAuth auth;

        [TestFixtureSetUp]
        public void SetUp()
        {
            auth = new PrivateAppAuth(appKey, appSecret);
        }

        [Test]
        public void Getoken()
        {
            Assert.NotNull(auth.GetToken());
        }
    }
}