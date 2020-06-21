using Newtonsoft.Json;

namespace ControleDeRelease.Share.Helper
{
    public static class JsonHelper
    {
        public static string Serialize(object obj) => JsonConvert.SerializeObject(obj);

        public static TEntity Deserialize<TEntity>(string value) => JsonConvert.DeserializeObject<TEntity>(value);
    }
}
