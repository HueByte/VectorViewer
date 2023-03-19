using VectorViewer.Models;
using Media = System.Windows.Media;
using System.Windows.Controls;
using Shapes = System.Windows.Shapes;

namespace VectorViewer.VectorShapes
{
    public sealed class CanvasCircle : ViewerShape
    {
        public Point Center { get; set; }
        public float Radius { get; set; }
        public bool Filled { get; set; }

        public override void Draw()
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
    }
}
