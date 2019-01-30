using System.ComponentModel;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace NetCore.App
{
    public interface IRequestProvider
    {
        T GetQueryValueOrDefault<T>(string key);
    }
    public class RequestProvider : IRequestProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RequestProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public T GetQueryValueOrDefault<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return default(T);
            }

            if (_httpContextAccessor?.HttpContext?.Request == null)
            {
                return default(T);
            }

            if (!_httpContextAccessor.HttpContext.Request.Query.Any(x => x.Key == key))
            {
                return default(T);
            }

            var result = _httpContextAccessor.HttpContext.Request.Query.FirstOrDefault(x => x.Key == key).Value;

            var converter = TypeDescriptor.GetConverter(typeof(T));
            try
            {
                return (T)converter.ConvertFromString(result);
            }
            catch
            {
                //Log the exception...
                return default(T);
            }
        }
    }
}
