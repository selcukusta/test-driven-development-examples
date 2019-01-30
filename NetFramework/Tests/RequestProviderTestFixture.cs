using Moq;
using NetFramework.App.Providers;
using NUnit.Framework;
using System.Collections.Specialized;
using System.Web;

namespace NetFramework.Tests
{
    [TestFixture]
    public class RequestProviderTestFixture
    {
        private HttpContextBase _httpContextBase;
        [OneTimeSetUp]
        public void Setup()
        {
            var queries = new NameValueCollection()
            {
                 { "a", "4"},
                 { "b", "test"},
                 { "c", "true"}
            };

            _httpContextBase = Mock.Of<HttpContextBase>
            (
                instance => instance.Request.QueryString == queries
            );
        }

        [TestCase("a", 4)]
        [TestCase("b", "test")]
        [TestCase("c", true)]
        public void GetQueryValueOrDefault_GivenActualCases_ReturnExpected<T>(string key, T expected)
        {
            var provider = new RequestProvider(_httpContextBase);
            var result = provider.GetQueryValueOrDefault<T>(key);
            Assert.AreEqual(result, expected);
        }
    }
}
