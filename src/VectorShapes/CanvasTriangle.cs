using VectorViewer.Models;
using Shapes = System.Windows.Shapes;
using Media = System.Windows.Media;

namespace VectorViewer.VectorShapes
{
    public sealed class CanvasTriangle : ViewerShape
    {
        public Point PointA { get; set; }
        public Point PointB { get; set; }
        public Point PointC { get; set; }
        public bool Filled { get; set; }

        public override void Draw()
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
    }
}
