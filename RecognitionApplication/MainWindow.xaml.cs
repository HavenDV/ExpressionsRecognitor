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

        public MainWindow()
        {
            InitializeComponent();
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
            var lines = SymbolFileParser.ParseFile(path);

            var text = string.Empty;
            foreach (var line in lines)
            {
                text += $"Symbol: {line.Symbol}, X: {line.X}, Y: {line.Y}, Width: {line.Width}, Height: {line.Height}{Environment.NewLine}";
            }

            FileTextBox.Text = text;

            var formula = LinesToWpfMathConverter.Convert(lines);
            FormulaTextBox.Text = formula;

            Canvas.Children.Clear();
            if (!File.Exists(ImagePath))
            {
                return;
            }

            using (var stream = new FileStream(ImagePath, FileMode.Open, FileAccess.Read))
            {
                var frame = BitmapFrame.Create(stream, BitmapCreateOptions.DelayCreation, BitmapCacheOption.None);
                var k = 1.0 * ImageGrid.ActualWidth / frame.PixelWidth;

                foreach (var line in lines)
                {
                    AddSelectionRectangle(
                        new Point(k * line.X, k * line.Y),
                        new Point(k * line.X2, k * line.Y2));
                }
            }
        }

        private void ShowImage(string path)
        {
            ImagePath = path;
            Image.Source = new BitmapImage(new Uri(path, UriKind.Absolute));
        }

        private void AddSelectionRectangle(Point point1, Point point2)
        {
            var topLeftPoint = new Point(Math.Min(point1.X, point2.X), Math.Min(point1.Y, point2.Y));

            var rectangle = new Rectangle
            {
                Stroke = Brushes.Red,
                StrokeThickness = 1,
                Margin = new Thickness(topLeftPoint.X, topLeftPoint.Y, topLeftPoint.X, topLeftPoint.Y),
                Width = Math.Abs(point2.X - point1.X),
                Height = Math.Abs(point2.Y - point1.Y)
            };
            Canvas.Children.Add(rectangle);
        }
    }
}
