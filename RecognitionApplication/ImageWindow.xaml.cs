using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RecognitionLibrary.Parsers;

namespace RecognitionApplication
{
    public partial class ImageWindow
    {
        public static void Show(string imagePath, string txtPath)
        {
            var window = new ImageWindow(imagePath, txtPath)
            {
                Image =
                {
                    Source = new BitmapImage(new Uri(imagePath, UriKind.Absolute))
                }
            };

            window.UpdateRectangles();

            window.Show();
        }

        private string ImagePath { get; }
        private string TxtPath { get; }

        public ImageWindow(string imagePath, string txtPath)
        {
            ImagePath = imagePath;
            TxtPath = txtPath;

            InitializeComponent();

            SizeChanged += (o, e) => UpdateRectangles();
        }

        private void UpdateRectangles()
        {
            Canvas.Children.Clear();
            if (!File.Exists(ImagePath) || !File.Exists(TxtPath))
            {
                return;
            }

            var lines = SymbolFileParser.ParseFile(TxtPath);
            var size = GetImageSize(ImagePath);
            var k = 1.0 * ImageGrid.ActualWidth / size.Width;

            foreach (var line in lines)
            {
                AddSelectionRectangle(
                    new Point(k * line.X, k * line.Y),
                    new Point(k * line.X2, k * line.Y2));
            }
        }

        private void AddSelectionRectangle(Point point1, Point point2)
        {
            var topLeftPoint = new Point(
                Math.Min(point1.X, point2.X),
                Math.Min(point1.Y, point2.Y));

            Canvas.Children.Add(new Rectangle
            {
                Stroke = Brushes.Red,
                StrokeThickness = 1,
                Margin = new Thickness(
                    topLeftPoint.X, topLeftPoint.Y,
                    topLeftPoint.X, topLeftPoint.Y),
                Width = Math.Abs(point2.X - point1.X),
                Height = Math.Abs(point2.Y - point1.Y)
            });
        }

        private static Size GetImageSize(string path)
        {
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var frame = BitmapFrame.Create(stream, BitmapCreateOptions.DelayCreation, BitmapCacheOption.None);

                return new Size(frame.PixelWidth, frame.PixelHeight);
            }
        }
    }
}
