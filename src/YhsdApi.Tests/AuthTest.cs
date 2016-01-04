﻿using System.Collections.Generic;
using NUnit.Framework;

namespace YhsdApi.Tests
{
    public class AuthTest
    {
        [TestFixtureSetUp]
        public void SetUp()
        {
            Configuration.AppKey = "790a310b972540de86b5c4817f04f459";
            Configuration.AppSecret = "4efdf06458ab4dd09d3972b83de7cd52";
            Configuration.CallLimitProtect = true;
        }

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
            Assert.IsTrue(new Auth().VerifyHmac(secret, param));
        }

        [Test]
        public void VerifyWebHook()
        {
            var token = "906155047ff74a14a1ca6b1fa74d3390";
            var data = "{\"created_at\":\"2014-08-28T17:28:13.301+08:00\",\"domain\":\"www.example.com\",\"enable_email_regist\":true,\"enable_mobile_regist\":true,\"enable_username_regist\":true,\"name\":\"TEST\",\"page_description\":\"\",\"page_title\":\"\",\"updated_at\":\"2015-07-27T13:58:14.607+08:00\",\"url\":\"http://w...content-available-to-author-only...e.com\",\"webhook_token\":\"906155047ff74a14a1ca6b1fa74d3390\"}";
            var hmac = "NS0Wcz2CDgzI4+L9/UYdwaXpPI4As7VD+wKCRgKqNUo=";
            Assert.IsTrue(new Auth().VerifyWebhook(token, data, hmac));
        }

        [Test]
        public void ThirdAppAesEncrypt()
        {
            var auth = new Auth();
            var aes = auth.ThirdAppAesEncrypt("{\"uid\":\"test@youhaosuda.com\",\"type\":\"email\",\"name\":\"test\"}", "095AE461E2554EED8D12F19F9662247E");
            Assert.AreEqual("mJgEpH-ja_sBlYG_W3HcbekE_HP2yQVrlX2hu8AKM8F5JjPFTRYBwc62HGhCZgfyf3FxECC9u-tcnmsZcheENw==", aes);
        }

        [Test]
        public void GetPrivateAppToken()
        {
            var auth = new Auth();
            Assert.NotNull(auth.GetPrivateAppToken());
        }

        [Test]
        public void GetPublicAppToken()
        {
            var auth = new Auth();
            Assert.NotNull(auth.GetPublicAppToken("http://example.com", "read_basic"));
        }

        [Test]
        public void GetPublicAppAuthorizeUrl()
        {
            var auth = new Auth();
            Assert.NotNull(auth.GetPublicAppAuthorizeUrl("http://example.com", "shop-key", ""));
        }
    }
}