using System.Text.Json.Serialization;
using VectorViewer.JsonConverters;
using Drawing = System.Drawing;

namespace VectorViewer.Models
{
    [JsonConverter(typeof(ColorConverter))]
    public struct Color
    {
        public byte A { get; set; }
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }

        public Drawing.Color ToDrawingColor() => Drawing.Color.FromArgb(A, R, G, B);
        public static Color FromDrawingColor(Drawing.Color color)
        {
            return new Color
            {
                A = color.A,
                R = color.R,
                G = color.G,
                B = color.B,
            };
        }
    }
}
