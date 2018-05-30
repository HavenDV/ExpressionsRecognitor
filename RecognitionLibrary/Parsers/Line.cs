using System;

namespace RecognitionLibrary.Parsers
{
    public class Line
    {
        public string Symbol { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public int X2 => X + Width;
        public int Y2 => Y + Height;

        public int CenterX => (int)Math.Round(X + 1.0 * Width / 2);
        public int CenterY => (int)Math.Round(Y + 1.0 * Height / 2);
    }
}