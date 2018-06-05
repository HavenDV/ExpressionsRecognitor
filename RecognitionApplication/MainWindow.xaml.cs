using System;
using System.Windows;
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
        private string Status { set => StatusTextBlock.Text = value; }

        #endregion

        #region Constructors

        public MainWindow()
        {
            InitializeComponent();
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

            ShowTextButton.IsEnabled = true;

            var lines = SymbolFileParser.ParseFile(TxtPath);
            var formula = LinesToWpfMathConverter.Convert(lines);

            FormulaTextBox.Text = formula;
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

            ShowImageButton.IsEnabled = true;

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

        private void ShowImageButton_OnClick(object sender, RoutedEventArgs e)
        {
            ImageWindow.Show(ImagePath, TxtPath);
        }

        #endregion

        #endregion
    }
}
