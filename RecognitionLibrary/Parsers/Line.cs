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

        public int X2
        {
            get { return X + Width; }
        }

        public int Y2
        {
            get { return Y + Height; }
        }

        public int CenterX
        {
            get { return (int) Math.Round(X + 1.0 * Width / 2); }
        }

        public int CenterY
        {
            get { return (int) Math.Round(Y + 1.0 * Height / 2); }
        }
    }
}