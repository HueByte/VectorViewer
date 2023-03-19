using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using VectorViewer.VectorShapes;

namespace VectorViewer.JsonConverters
{
    public class ShapeConverter : JsonConverter<ViewerShape>
    {
        private readonly Dictionary<string, Type> _types;
        public ShapeConverter()
        {
            _types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(ViewerShape).IsAssignableFrom(p)
                    && p.IsClass
                    && !p.IsAbstract
                    && p.GetCustomAttribute<ShapeTypeAttribute>() is not null)
                .ToDictionary(type => type.GetCustomAttribute<ShapeTypeAttribute>()!.ShapeType, type => type);
        }

        public override ViewerShape? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException();

            using var jsonDocument = JsonDocument.ParseValue(ref reader);

            if (!jsonDocument.RootElement.TryGetProperty(nameof(ViewerShape.Type).ToLower(), out var typeProperty))
                throw new JsonException();

            string propType = typeProperty.GetString()!.ToLower();
            if(string.IsNullOrEmpty(propType) || !_types.ContainsKey(propType))
            {
                throw new JsonException();
            }

            var jsonString = jsonDocument.RootElement.GetRawText();
            var jsonObject = (ViewerShape)JsonSerializer.Deserialize(jsonString, _types[propType], options)!;
            
            return jsonObject;
        }

        public override void Write(Utf8JsonWriter writer, ViewerShape value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
