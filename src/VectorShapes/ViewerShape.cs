using System.Text.Json.Serialization;
using System.Windows.Controls;
using VectorViewer.Models;

namespace VectorViewer.VectorShapes
{
    public abstract class ViewerShape
    {
        [JsonIgnore]
        public Canvas Canvas { get; set; } = default!;
        public string Type { get; set; } = default!;
        public Color Color { get; set; }

        public abstract void Draw();
    }
}
