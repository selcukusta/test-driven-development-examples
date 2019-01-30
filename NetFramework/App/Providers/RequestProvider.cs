using System.ComponentModel;
using System.Web;

namespace NetFramework.App.Providers
{
    public class RequestProvider
    {
        private readonly HttpContextBase _httpContextBase;
        public RequestProvider(HttpContextBase httpContextBase)
        {
            _httpContextBase = httpContextBase;
        }

        public T GetQueryValueOrDefault<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return default(T);
            }

            if (_httpContextBase?.Request?.QueryString?[key] == null)
            {
                return default(T);
            }

            var result = _httpContextBase.Request.QueryString[key];

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