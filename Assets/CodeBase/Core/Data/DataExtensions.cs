using Newtonsoft.Json;
using UnityEngine;

namespace CodeBase.Core.Data
{
    public static class DataExtensions
    {
        public static string ToJson(this object obj) => 
            JsonConvert.SerializeObject(obj, Formatting.Indented);

        public static T ToDeserialized<T>(this string json) =>
            JsonUtility.FromJson<T>(json);
    }
}