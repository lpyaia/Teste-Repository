using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace HBSIS.Framework.Commons.Helper
{
    public static class JsonHelper
    {
        public static JsonSerializerSettings GetSettings()
        {
            return new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                FloatFormatHandling = FloatFormatHandling.String,
                FloatParseHandling = FloatParseHandling.Decimal,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };
        }

        public static string Serialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, GetSettings());
        }

        public static string SerializeToLowerCamelCase<T>(T obj)
        {
            var settings = GetSettings();
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            return JsonConvert.SerializeObject(obj, settings);
        }

        public static T DeserializeFromLowerCamelCase<T>(string json)
        {
            var settings = GetSettings();
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            return JsonConvert.DeserializeObject<T>(json, settings);
        }

        public static string JsonSerialize<T>(this T obj)
        {
            return Serialize(obj);
        }

        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, GetSettings());
        }

        public static T JsonDeserialize<T>(this string json)
        {
            return Deserialize<T>(json);
        }
        
        public static bool TryJsonDeserialize<T>(this string json, out T @object)
        {
            try
            {
                @object = Deserialize<T>(json);
                return true;
            }
            catch (Exception)
            {
                @object = default(T);
                return false;
            }
        }

        public static object Deserialize(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type, GetSettings());
        }

        public static object JsonDeserialize(string json, Type type)
        {
            return Deserialize(json, type);
        }
    }
}