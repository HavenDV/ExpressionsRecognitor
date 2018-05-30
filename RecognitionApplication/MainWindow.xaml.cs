using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using RecognitionLibrary.Converters;
using RecognitionLibrary.Parsers;

namespace RecognitionApplication
{
    public partial class MainWindow
    {
        private string ImagePath { get; set; }
        private string TxtPath { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            SizeChanged += (o, e) => UpdateRectangles();
        }

        private void OpenTxtFileButton_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() != true)
            {
                return;
            }

            var path = dialog.FileName;

            Parse(path);
        }

        private void OpenImageButton_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() != true)
            {
                return;
            }

            var path = dialog.FileName;

            ShowImage(path);
        }

        private void FormulaTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            FormulaControl.Formula = FormulaTextBox.Text;
        }

        private void Parse(string path)
        {
            TxtPath = path;

            var lines = SymbolFileParser.ParseFile(path);

            var text = string.Empty;
            foreach (var line in lines)
            {
                text += $"Symbol: {line.Symbol}, X: {line.X}, Y: {line.Y}, Width: {line.Width}, Height: {line.Height}{Environment.NewLine}";
            }

            FileTextBox.Text = text;

            var formula = LinesToWpfMathConverter.Convert(lines);
            FormulaTextBox.Text = formula;

            UpdateRectangles();
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

        private void ShowImage(string path)
        {
            ImagePath = path;

            Image.Source = new BitmapImage(new Uri(path, UriKind.Absolute));

            UpdateRectangles();
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
