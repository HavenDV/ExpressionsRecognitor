using System;
using RecognitionLibrary.Parsers;

namespace RecognitionApplication
{
    public partial class TextWindow
    {
        public static void Show(string path)
        {
            var lines = SymbolFileParser.ParseFile(path);

            var text = string.Empty;
            foreach (var line in lines)
            {
                text += $"Symbol: {line.Symbol}, X: {line.X}, Y: {line.Y}, Width: {line.Width}, Height: {line.Height}{Environment.NewLine}";
            }

            var window = new TextWindow
            {
                TextBox =
                {
                    Text = text
                }
            };

            window.Show();
        }

        public TextWindow()
        {
            InitializeComponent();
        }
    }
}
