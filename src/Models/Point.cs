using System.Text.Json.Serialization;
using VectorViewer.JsonConverters;

namespace VectorViewer.Models
{
    [JsonConverter(typeof(PointConverter))]
    public struct Point
    {
        public float X { get; set; }
        public float Y { get; set; }

        public System.Windows.Point ToWindowsPoint() => new() { X = X, Y = Y };
    }
}
