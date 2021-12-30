﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EliteAPI.Events;

// Converts "Message": "symbol", "Message_Localised": "value"
//       to "Message": { Symbol: "symbol", "Local": "value" }

public class LocalisedConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object? rawValue, JsonSerializer serializer)
    {
        if (rawValue == null)
            return;

        var value = (Localised) rawValue;
        writer.WriteValue(value.Symbol);

        var name = writer.Path.Split('.').Last();

        writer.WritePropertyName($"{name}_Localised");
        writer.WriteValue(value.Local);
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? rawValue, JsonSerializer serializer)
    {
        var symbol = JToken.Load(reader).ToString();

        reader.Read();
        var localised = reader.ReadAsString();

        return new Localised(symbol, localised ?? string.Empty);
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(Localised);
    }
}