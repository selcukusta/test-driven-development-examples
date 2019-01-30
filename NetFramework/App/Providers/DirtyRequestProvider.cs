using System.ComponentModel;
using System.Web;

namespace NetFramework.App.Providers
{
    public class DirtyRequestProvider 
    {
        public T GetQueryValueOrDefault<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return default(T);
            }

            if (HttpContext.Current?.Request?.QueryString?[key] == null)
            {
                return default(T);
            }

            var result = HttpContext.Current.Request.QueryString[key];

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