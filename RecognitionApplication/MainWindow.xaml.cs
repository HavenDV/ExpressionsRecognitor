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
        #region Properties

        private string TxtPath { get; set; }
        private string ImagePath { get; set; }
        private string Status
        {
            set { StatusTextBlock.Text = value; }
        }

        #endregion

        #region Constructors

        public MainWindow()
        {
            InitializeComponent();

            SizeChanged += (o, e) => UpdateRectangles();
        }

        #endregion
        
        #region Event Handlers

        private void FormulaTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            FormulaControl.Formula = FormulaTextBox.Text;
        }

        #region Open Buttons

        private void OpenTextFileButton_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
            };
            if (dialog.ShowDialog() != true)
            {
                return;
            }

            TxtPath = dialog.FileName;

            ShowImage();

            ShowTextFileMenuItem.IsEnabled = true;
            ShowTextFileResultMenuItem.IsEnabled = true;
        }

        private async void OpenImageButton_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*"
            };
            if (dialog.ShowDialog() != true)
            {
                return;
            }

            ImagePath = dialog.FileName;

            ShowImage();

            Status = "Converting...";

            try
            {
                var latex = await ImageToLatexConverter.ConvertAsync(ImagePath);

                FormulaTextBox.Text = latex;

                Status = "Converted!";
            }
            catch (Exception exception)
            {
                Status = $"Convert error: {exception.Message}";
            }
        }

        #endregion

        #region Show Buttons

        private void ShowTextFileButton_OnClick(object sender, RoutedEventArgs e)
        {
            TextWindow.Show(TxtPath);
        }

        private void ShowTextFileResultButton_OnClick(object sender, RoutedEventArgs e)
        {
            var lines = SymbolFileParser.ParseFile(TxtPath);
            var formula = LinesToWpfMathConverter.Convert(lines);

            FormulaTextBox.Text = formula;
        }
        
        #endregion

        private void ExitMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion

        #region Show Image

        private void ShowImage()
        {
            if (File.Exists(ImagePath))
            {
                Image.Source = new BitmapImage(new Uri(ImagePath, UriKind.Absolute));
            }

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

        #endregion
    }
}
