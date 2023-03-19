using Drawing = System.Drawing;
using Media = System.Windows.Media;
using Shapes = System.Windows.Shapes;
using System.Windows;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using VectorViewer.VectorShapes;
using VectorViewer.Models;
using System.Linq;
using System.Transactions;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Win32;
using System.IO;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace VectorViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<ViewerShape> Shapes { get; set; } = new();
        private double _scaleFactor = 1;
        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            Container.Background = new Media.BrushConverter().ConvertFromString("#001220") as Media.Brush;
            //RandomSeed();

            InputManager inputManager = new();
            var inputShapes = inputManager.ParseInput();

            foreach(var shape in inputShapes)
            {
                shape.Canvas = MainCanvas;
            }

            Shapes.AddRange(inputShapes);

            Loaded += (sender, args) => Dispatcher.BeginInvoke(async () => await Draw());
            Application.Current.MainWindow.SizeChanged += (sender, args) => Dispatcher.BeginInvoke(() => ScaleCanvas());
        }

        private void ScaleCanvas()
        {
            float maxHorizontalDistance = 0;
            float maxVerticalDistance = 0;

            foreach (var shape in Shapes)
            {
                float tempX = shape.GetfarthestX();
                if (tempX > maxHorizontalDistance) maxHorizontalDistance = tempX;

                float tempY = shape.GetfarthestY();
                if (tempY > maxVerticalDistance) maxVerticalDistance = tempY;
            }

            double containerH = Container.ActualHeight / 2;
            double containerW = Container.ActualWidth / 2;

            bool isHorizontalOverflow = maxHorizontalDistance > containerW;
            bool isVerticalOverflow = maxVerticalDistance > containerH;

            if (isHorizontalOverflow && isVerticalOverflow)
            {
                if (maxHorizontalDistance >= maxVerticalDistance)
                {
                    _scaleFactor = containerW / maxHorizontalDistance;
                }
                else
                {
                    _scaleFactor = containerH / maxVerticalDistance;
                }
            }
            else if (isHorizontalOverflow)
            {
                _scaleFactor = containerW / maxHorizontalDistance;
            }
            else if (isVerticalOverflow)
            {
                _scaleFactor = containerH / maxVerticalDistance;
            }

            if (_scaleFactor < 1)
            {
                MainCanvas.LayoutTransform = new ScaleTransform(_scaleFactor, _scaleFactor);
            }
        }

        private async Task Draw()
        {
            MainCanvas.Children.Clear();
            ScaleCanvas();
            foreach (var shape in Shapes) { shape.Draw(); await Task.Delay(100); }
        }

        private void RandomSeed()
        {
            int shapeCount = 50;
            int coordMin = -500;
            int coordMax = 500;
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

            CanvasLine line1 = new()
            {
                Canvas = MainCanvas,
                Color = Models.Color.FromDrawingColor(colours[rnd.Next(0, colours.Count)]),
                Start = new() { X = 0, Y = 0 },
                End = new() { X = 0, Y = 600 },
            };

            CanvasLine line2 = new()
            {
                Canvas = MainCanvas,
                Color = Models.Color.FromDrawingColor(colours[rnd.Next(0, colours.Count)]),
                Start = new() { X = 0, Y = 0 },
                End = new() { X = 20, Y = 0 },
            };

            Shapes.Add(line1);
            Shapes.Add(line2);

            for (int i = 0; i < shapeCount; i++)
            {
                var line = new CanvasLine()
                {
                    Canvas = MainCanvas,
                    Color = Models.Color.FromDrawingColor(colours[rnd.Next(0, colours.Count)]),
                    Start = new() { X = rnd.Next(coordMin, coordMax), Y = rnd.Next(coordMin, coordMax) },
                    End = new() { X = rnd.Next(coordMin, coordMax), Y = rnd.Next(coordMin, coordMax) },
                };

                var triangle = new CanvasTriangle()
                {
                    Canvas = MainCanvas,
                    Color = Models.Color.FromDrawingColor(colours[rnd.Next(0, colours.Count)]),
                    PointA = new() { X = rnd.Next(coordMin, coordMax), Y = rnd.Next(coordMin, coordMax) },
                    PointB = new() { X = rnd.Next(coordMin, coordMax), Y = rnd.Next(coordMin, coordMax) },
                    PointC = new() { X = rnd.Next(coordMin, coordMax), Y = rnd.Next(coordMin, coordMax) },
                    Filled = rnd.Next(0, 100) > 95,
                };

                var circle = new CanvasCircle()
                {
                    Canvas = MainCanvas,
                    Color = Models.Color.FromDrawingColor(colours[rnd.Next(0, colours.Count)]),
                    Center = new() { X = rnd.Next(coordMin, coordMax), Y = rnd.Next(coordMin, coordMax) },
                    Radius = rnd.Next(0, 50),
                    Filled = rnd.Next(0, 100) > 85,
                };

                Shapes.Add(line);
                Shapes.Add(triangle);
                Shapes.Add(circle);
            }
        }
    }
}