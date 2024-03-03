using Newtonsoft.Json;

namespace CodeBase.Core.Data
{
    public static class DataExtensions
    {
        public static string ToJson(this object obj) => 
            JsonConvert.SerializeObject(obj, Formatting.Indented);

        public static T ToDeserialized<T>(this string serializedString) =>
            JsonConvert.DeserializeObject<T>(serializedString);
    }
}