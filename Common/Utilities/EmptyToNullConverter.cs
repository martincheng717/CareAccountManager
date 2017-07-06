using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Gdot.Care.Common.Utilities
{
    [ExcludeFromCodeCoverage]
    public class EmptyToNullConverter : JsonConverter
    {
        private readonly JsonSerializer _stringSerializer = new JsonSerializer();

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (string);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            string value = _stringSerializer.Deserialize<string>(reader);

            if (string.IsNullOrEmpty(value))
            {
                value = null;
            }

            return value;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            _stringSerializer.Serialize(writer, value);
        }
    }
}
