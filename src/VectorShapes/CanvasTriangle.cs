using VectorViewer.Models;
using Shapes = System.Windows.Shapes;
using Media = System.Windows.Media;
using System;
using System.Text.Json.Serialization;

namespace VectorViewer.VectorShapes
{
    [ShapeType("triangle")]
    public sealed class CanvasTriangle : ViewerShape
    {
        public override string Type { get; } = "Triangle";

        [JsonPropertyName("a")]
        public Point PointA { get; set; }

        [JsonPropertyName("b")]
        public Point PointB { get; set; }

        [JsonPropertyName("c")]
        public Point PointC { get; set; }

        [JsonPropertyName("filled")]
        public bool Filled { get; set; }

        public override void Draw(double scale = 1)
        {
            Shapes.Polygon poly = new()
            {
                Points = new()
                {
                    new System.Windows.Point() { X = PointA.X, Y = PointA.Y },
                    new System.Windows.Point() { X = PointB.X, Y = PointB.Y },
                    new System.Windows.Point() { X = PointC.X, Y = PointC.Y }
                },
                Stroke = new Media.SolidColorBrush(Media.Color.FromArgb(Color.A, Color.R, Color.G, Color.B)),
                Fill = Filled ? new Media.SolidColorBrush(Media.Color.FromArgb(Color.A, Color.R, Color.G, Color.B)) : null
            };

            Canvas.Children.Add(poly);
        }

        public override float GetfarthestX() => Math.Max(Math.Max(Math.Abs(PointA.X), Math.Abs(PointB.X)), Math.Abs(PointC.X));

        public override float GetfarthestY() => Math.Max(Math.Max(Math.Abs(PointA.Y), Math.Abs(PointB.Y)), Math.Abs(PointC.Y));
    }
}
