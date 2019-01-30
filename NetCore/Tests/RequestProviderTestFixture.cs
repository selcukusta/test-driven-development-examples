using NUnit.Framework;
using Moq;
using NetCore.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;

namespace NetCore.Tests
{
    [TestFixture]
    public class RequestProviderTestFixture
    {
        private IHttpContextAccessor _httpContextAccessor;
        [OneTimeSetUp]
        public void Setup()
        {
            var queries = new Dictionary<string, StringValues>()
            {
                 { "a", new StringValues("4")},
                 { "b", new StringValues("test")},
                 { "c", new StringValues("true")}
            };
            QueryCollection c = new QueryCollection(queries);

            _httpContextAccessor = Mock.Of<IHttpContextAccessor>
            (
                instance => instance.HttpContext.Request.Query == c
            );
        }

        [TestCase("a", 4)]
        [TestCase("b", "test")]
        [TestCase("c", true)]
        public void GetQueryValueOrDefault_GivenActualCases_ReturnExpected<T>(string key, T expected)
        {
            var provider = new RequestProvider(_httpContextAccessor);
            var result = provider.GetQueryValueOrDefault<T>(key);
            Assert.AreEqual(result, expected);
        }
    }
}
