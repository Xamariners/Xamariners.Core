using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Xamariners.Core.Common.Infrastructure.Serialisers
{
    public class JsonConcreteConverter<T> : JsonConverter
    {
        public override bool CanConvert(Type objectType) => true;


        public override object ReadJson(JsonReader reader,
         Type objectType, object existingValue, JsonSerializer serializer)
        {
            serializer.TypeNameHandling = TypeNameHandling.Auto;
            serializer.MaxDepth = 3;
            serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return serializer.Deserialize<T>(reader);
        }

        public override void WriteJson(JsonWriter writer,
            object value, JsonSerializer serializer)
        {
            serializer.TypeNameHandling = TypeNameHandling.Auto;
            serializer.MaxDepth = 3;
            serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            serializer.Serialize(writer, value);
        }
    }
}
