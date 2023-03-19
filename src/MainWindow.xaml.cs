using Drawing = System.Drawing;
using Media = System.Windows.Media;
using Shapes = System.Windows.Shapes;
using System.Windows;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using VectorViewer.VectorShapes;
using VectorViewer.Models;

namespace VectorViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<ViewerShape> Shapes { get; set; } = new();
        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            MainCanvas.Background = new Media.BrushConverter().ConvertFrom("#001220") as Media.Brush;
            Dispatcher.BeginInvoke(async () => await DrawRandomSeed());
        }

        private async Task DrawRandomSeed()
        {
            int shapeCount = 100;
            int coordMin = 0;
            int coordMax = 900;
            Random rnd = new();
            List<Drawing.Color> colours = new()
            {
                Drawing.Color.Crimson,
                Drawing.Color.Aquamarine,
                Drawing.Color.SeaGreen,
                Drawing.Color.Aquamarine,
                Drawing.Color.DarkOrange,
                Drawing.Color.DarkViolet,
            };

            for (int i = 0; i < shapeCount; i++)
            {
                var line = new CanvasLine()
                {
                    Canvas = MainCanvas,
                    Color = Color.FromDrawingColor(colours[rnd.Next(0, colours.Count)]),
                    Start = new() { X = rnd.Next(coordMin, coordMax), Y = rnd.Next(coordMin, coordMax) },
                    End = new() { X = rnd.Next(coordMin, coordMax), Y = rnd.Next(coordMin, coordMax) },
                    Type = ""
                };

                var triangle = new CanvasTriangle()
                {
                    Canvas = MainCanvas,
                    Color = Color.FromDrawingColor(colours[rnd.Next(0, colours.Count)]),
                    PointA = new() { X = rnd.Next(coordMin, coordMax), Y = rnd.Next(coordMin, coordMax) },
                    PointB = new() { X = rnd.Next(coordMin, coordMax), Y = rnd.Next(coordMin, coordMax) },
                    PointC = new() { X = rnd.Next(coordMin, coordMax), Y = rnd.Next(coordMin, coordMax) },
                    Filled = rnd.Next(0, 2) == 1,
                    Type = ""
                };

                var circle = new CanvasCircle()
                {
                    Canvas = MainCanvas,
                    Color = Color.FromDrawingColor(colours[rnd.Next(0, colours.Count)]),
                    Center = new() { X = rnd.Next(coordMin, coordMax), Y = rnd.Next(coordMin, coordMax) },
                    Radius = rnd.Next(0, 50),
                    Filled = rnd.Next(0, 2) == 1,
                    Type = ""
                };

                Shapes.Add(line);
                Shapes.Add(triangle);
                Shapes.Add(circle);
            }

            foreach (var shape in Shapes)
            {
                shape.Draw();
                await Task.Delay(50);
            }
        }
    }
}