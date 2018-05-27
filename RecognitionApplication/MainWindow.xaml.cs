using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using RecognitionLibrary;

namespace RecognitionApplication
{
    public partial class MainWindow
    {
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
        }

        private void ShowImage(string path)
        {
            Image.Source = new BitmapImage(new Uri(path, UriKind.Absolute));
        }
    }
}
