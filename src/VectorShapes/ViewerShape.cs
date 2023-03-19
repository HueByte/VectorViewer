using System.Text.Json.Serialization;
using System.Windows.Controls;
using VectorViewer.JsonConverters;
using VectorViewer.Models;

namespace VectorViewer.VectorShapes
{
    [JsonConverter(typeof(ShapeConverter))]
    public abstract class ViewerShape
    {
        [JsonIgnore]
        public Canvas Canvas { get; set; } = default!;
        public abstract string Type { get; }

        [JsonPropertyName("color")]
        public Color Color { get; set; }

        public abstract void Draw(double scale = 1);
        public abstract float GetfarthestX();
        public abstract float GetfarthestY();
    }
}
