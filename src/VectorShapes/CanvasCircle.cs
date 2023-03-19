using VectorViewer.Models;
using Media = System.Windows.Media;
using System.Windows.Controls;
using Shapes = System.Windows.Shapes;
using System;
using System.Text.Json.Serialization;

namespace VectorViewer.VectorShapes
{
    [ShapeType("circle")]
    public sealed class CanvasCircle : ViewerShape
    {
        public override string Type => "Circle";

        [JsonPropertyName("center")]
        public Point Center { get; set; }

        [JsonPropertyName("radius")]
        public float Radius { get; set; }

        public bool Filled { get; set; }

        public override void Draw(double scale = 1)
        {
            Shapes.Ellipse ellipse = new()
            {
                Width = Radius * 2,
                Height = Radius * 2,
                Stroke = new Media.SolidColorBrush(Media.Color.FromArgb(Color.A, Color.R, Color.G, Color.B)),
                Fill = Filled ? new Media.SolidColorBrush(Media.Color.FromArgb(Color.A, Color.R, Color.G, Color.B)) : null
            };

            Canvas.Children.Add(ellipse);
            Canvas.SetLeft(ellipse, Center.X - Radius * 2);
            Canvas.SetTop(ellipse, Center.X - Radius * 2);
        }
        public override float GetfarthestX() => Math.Abs(Center.X) + Radius;
        public override float GetfarthestY() => Math.Abs(Center.Y) + Radius;
    }
}
