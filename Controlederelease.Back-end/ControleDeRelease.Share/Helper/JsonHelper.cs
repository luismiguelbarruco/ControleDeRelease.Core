using Newtonsoft.Json;

namespace ControleDeRelease.Share.Helper
{
    public static class JsonHelper
    {
        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static TEntity Deserialize<TEntity>(string value)
        {
            return JsonConvert.DeserializeObject<TEntity>(value);
        }
    }
}
