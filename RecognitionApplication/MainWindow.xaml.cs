using System;
using System.Windows;
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

        private void OpenFileButton_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() != true)
            {
                return;
            }

            var path = dialog.FileName;
            var lines = SymbolFileParser.ParseFile(path);

            var text = string.Empty;
            foreach (var line in lines)
            {
                text += $"Symbol: {line.Symbol}, X: {line.X}, Y: {line.Y}, Width: {line.Width}, Height: {line.Height}{Environment.NewLine}";
            }

            FileTextBox.Text = text;
        }
    }
}
