using System;
using System.Text.Json.Serialization;
using System.Text.Json;
using VectorViewer.Models;

namespace VectorViewer.JsonConverters
{
    class ColorConverter : JsonConverter<Color>
    {
        public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException($"Unexpected token type '{reader.TokenType}' when reading Color.");
            }

            var rawValue = reader.GetString();
            var value = rawValue.Trim().Split(';');

            if (value.Length != 4 ||
                !byte.TryParse(value[0], out byte a) ||
                !byte.TryParse(value[1], out byte r) ||
                !byte.TryParse(value[2], out byte g) ||
                !byte.TryParse(value[3], out byte b))
            {
                throw new JsonException($"Invalid Color format: {rawValue}.");
            }

            return new Color { A = a, R = r, G = g, B = b };
        }

        public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
        {
            writer.WriteStringValue($"{value.A};{value.R};{value.G};{value.B}");
        }
    }
}
