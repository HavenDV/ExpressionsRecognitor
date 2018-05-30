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
    }
}