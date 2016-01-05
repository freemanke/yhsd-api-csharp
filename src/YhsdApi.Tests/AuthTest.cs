using System.Collections.Generic;
using NUnit.Framework;

namespace YhsdApi.Tests
{
    public class AuthTestTest
    {
        [Test]
        public void VerifyHmac()
        {
            var secret = "hush";
            var param = new Dictionary<string, string>
            {
                {"shop_key", "a94a110d86d2452eb3e2af4cfb8a3828"},
                {"code", "a84a110d86d2452eb3e2af4cfb8a3828"},
                {"account_id", "1"},
                {"time_stamp", "2013-08-27T13:58:35Z"},
                {"hmac", "a2a3e2dcd8a82fd9070707d4d921ac4cdc842935bf57bc38c488300ef3960726"}
            };
            Assert.IsTrue(Auth.VerifyHmac(secret, param));
        }

        [Test]
        public void VerifyWebhook()
        {
            var token = "906155047ff74a14a1ca6b1fa74d3390";
            var data = "data";
            var hmac = "3iAqy/BJfK2U3VU77vAPsGXmSnJqNVqurqHJeho326Q=";
            Assert.IsTrue(Auth.VerifyWebhook(token, data, hmac));
        }

        [Test]
        public void ThirdAppAesEncrypt()
        {
            var aes = Auth.ThirdAppAesEncrypt("{\"uid\":\"test@youhaosuda.com\",\"type\":\"email\",\"name\":\"test\"}", "095AE461E2554EED8D12F19F9662247E");
            Assert.AreEqual("mJgEpH-ja_sBlYG_W3HcbekE_HP2yQVrlX2hu8AKM8F5JjPFTRYBwc62HGhCZgfyf3FxECC9u-tcnmsZcheENw==", aes);
        }
    }
}