using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Newtonsoft.Json;

namespace Xamariners.Core.Model.Converters
{
    public class SingleValueArrayConverter : JsonConverter<ICollection<string>>
    {
        public override void WriteJson(JsonWriter writer, ICollection<string> value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override ICollection<string> ReadJson(JsonReader reader, Type objectType, ICollection<string> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            Collection<string> retVal = new Collection<string>();
            if (reader.TokenType == JsonToken.StartObject)
            {
                string instance = (string)serializer.Deserialize(reader, typeof(string));
                retVal = new Collection<string>() { instance };
            }
            else if (reader.TokenType == JsonToken.StartArray)
            {
                retVal = serializer.Deserialize<Collection<string>>(reader);

            }
            return retVal;
        }
    }
}
