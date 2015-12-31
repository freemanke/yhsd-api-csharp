using NUnit.Framework;

namespace YhsdApi.Tests
{
    public class ConfigurationTest
    {
        [Test]
        public void DefaultValue()
        {
            Assert.AreEqual("https://apps.youhaosuda.com/oauth2/token/", Configuration.TokenUrl);
            Assert.AreEqual("https://api.youhaosuda.com/", Configuration.ApiUrl);
            Assert.AreEqual("https://apps.youhaosuda.com/oauth2/authorize/", Configuration.AuthUrl);
            Assert.AreEqual("v1/", Configuration.ApiVersion);
        }
    }
}