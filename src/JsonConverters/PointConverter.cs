using System;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using VectorViewer.Models;
using System.Buffers;
using System.Globalization;

namespace VectorViewer.JsonConverters
{
    class PointConverter : JsonConverter<Point>
    {
        public override Point Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string[] values = reader.GetString().Split(';');
            float x = float.Parse(values[0].Replace(",", "."));
            float y = float.Parse(values[1].Replace(",", "."));
            return new Point() { X = x, Y = y };
        }

        public override void Write(Utf8JsonWriter writer, Point value, JsonSerializerOptions options)
        {
            writer.WriteStringValue($"{value.X},{value.Y}");
        }
    }
}
