using VectorViewer.Models;
using Shapes = System.Windows.Shapes;
using Media = System.Windows.Media;

namespace VectorViewer.VectorShapes
{
    public sealed class CanvasLine : ViewerShape
    {
        public Point Start { get; set; }
        public Point End { get; set; }

        public CanvasLine() { }

        public override void Draw()
        {
            Shapes.Line line = new()
            {
                X1 = Start.X,
                Y1 = Start.Y,
                X2 = End.X,
                Y2 = End.Y,
                Stroke = new Media.SolidColorBrush(Media.Color.FromArgb(Color.A, Color.R, Color.G, Color.B)),
            };

            Canvas.Children.Add(line);
        }
    }
}
