using VectorViewer.Models;
using Shapes = System.Windows.Shapes;
using Media = System.Windows.Media;
using System;
using System.Text.Json.Serialization;

namespace VectorViewer.VectorShapes
{
    [ShapeType("line")]
    public sealed class CanvasLine : ViewerShape
    {
        public override string Type => "Line";

        [JsonPropertyName("a")]
        public Point Start { get; set; }

        [JsonPropertyName("b")]
        public Point End { get; set; }

        public CanvasLine() { }

        public override void Draw(double scale = 1)
        {
            Shapes.Line line = new()
            {
                X1 = Start.X * scale,
                Y1 = Start.Y * scale,
                X2 = End.X * scale,
                Y2 = End.Y * scale,
                Stroke = new Media.SolidColorBrush(Media.Color.FromArgb(Color.A, Color.R, Color.G, Color.B)),
            };

            Canvas.Children.Add(line);
        }

        public override float GetfarthestX() => Math.Max(Math.Abs(Start.X), Math.Abs(End.X));
        public override float GetfarthestY() => Math.Max(Math.Abs(Start.Y), Math.Abs(End.Y));
    }
}
