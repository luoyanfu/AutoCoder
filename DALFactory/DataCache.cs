using System.Web;

namespace DALFactory
{
    /// <summary>
    /// data cache
    /// </summary>
    public class DataCache
    {
        public static object GetCache(string cacheKey)
        {
            var objCache = HttpRuntime.Cache;
            return objCache[cacheKey];
        }

        public static void SetCache(string cacheKey, object objObject)
        {
            var objCache = HttpRuntime.Cache;
            objCache.Insert(cacheKey, objObject);
        }
    }
}