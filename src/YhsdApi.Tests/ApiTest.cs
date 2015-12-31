using System;
using System.Net;
using NUnit.Framework;

namespace YhsdApi.Tests
{
    public class ApiTest
    {
        [TestFixtureSetUp]
        public void SetUp()
        {
            Configuration.AppKey = "790a310b972540de86b5c4817f04f459";
            Configuration.AppSecret = "4efdf06458ab4dd09d3972b83de7cd52";
            Configuration.CallLimitProtect = true;
        }

        [Test]
        public void Get()
        {
            var api = new Api(new Auth().GetPrivateAppToken());
            var response = api.Get("countries");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void Post()
        {
            var api = new Api(new Auth().GetPrivateAppToken());
            var response = api.Post("customers", new
            {
                customer = new
                {
                    reg_type = "email",
                    reg_identity = Guid.NewGuid() + "@example.com",
                    password = "123456",
                    notify_email = "for@example.com",
                    notify_phone = "13632269380"
                }
            });
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void Put()
        {
            var api = new Api(new Auth().GetPrivateAppToken());
            var response = api.Put("customers/15924", new
            {
                customer = new
                {
                    reg_type = "email",
                    reg_identity = Guid.NewGuid() + "@example.com",
                    password = "123456",
                    notify_email = "for@example.com",
                    notify_phone = "13632269380"
                }
            });
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void Delete()
        {
            var api = new Api(new Auth().GetPrivateAppToken());
            var response = api.Get("customers/1");
            Assert.AreEqual((HttpStatusCode) 422, response.StatusCode);
        }
    }
}