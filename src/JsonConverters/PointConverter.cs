using System;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using VectorViewer.Models;
using System.Buffers;

namespace VectorViewer.JsonConverters
{
    class PointConverter : JsonConverter<Point>
    {
        public override Point Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException($"Unexpected token type '{reader.TokenType}' when reading Point.");
            }

            ReadOnlySpan<byte> valueSpan = reader.HasValueSequence ? reader.ValueSequence.ToArray() : reader.ValueSpan;

            int separatorIndex = valueSpan.IndexOf((byte)';');
            int commaIndex = valueSpan.IndexOf((byte)',');
            int lastIndex = valueSpan.Length - 1;

            if (separatorIndex == -1 || commaIndex == -1 || commaIndex > separatorIndex || separatorIndex == lastIndex)
            {
                throw new JsonException($"Invalid Point format: {reader.GetString()}.");
            }

            int xIndex = 0;
            while (xIndex < separatorIndex && valueSpan[xIndex] == (byte)' ')
            {
                xIndex++;
            }

            if (!long.TryParse(valueSpan.Slice(xIndex, commaIndex - xIndex).ToString(), out var x))
            {
                throw new JsonException($"Could not parse X value from Point format: {reader.GetString()}.");
            }

            int yIndex = separatorIndex + 1;
            while (yIndex < lastIndex && valueSpan[yIndex] == (byte)' ')
            {
                yIndex++;
            }

            if (!long.TryParse(valueSpan.Slice(yIndex, lastIndex - yIndex).ToString(), out var y))
            {
                throw new JsonException($"Could not parse Y value from Point format: {reader.GetString()}.");
            }

            return new Point { X = x, Y = y };
        }

        public override void Write(Utf8JsonWriter writer, Point value, JsonSerializerOptions options)
        {
            writer.WriteStringValue($"{value.X},{value.Y}");
        }
    }
}
