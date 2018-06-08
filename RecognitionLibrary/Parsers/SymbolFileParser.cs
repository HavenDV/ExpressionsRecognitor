using System;
using System.Collections.Generic;
using System.IO;

namespace RecognitionLibrary.Parsers
{
    public static class SymbolFileParser
    {
        public static List<Line> Parse(string[] fileLines)
        {
            var lines = new List<Line>();
            foreach (var fileLine in fileLines)
            {
                var index = fileLine.IndexOf(' ');
                var symbol = fileLine.Substring(0, index);
                var values = fileLine
                    .Substring(index + 2, fileLine.Length - index - 3)
                    .Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                lines.Add(new Line
                {
                    Symbol = symbol,
                    X = int.Parse(values[0]),
                    Y = int.Parse(values[1]),
                    Width = int.Parse(values[2]),
                    Height = int.Parse(values[3])
                });
            }

            return lines;
        }

        public static List<Line> ParseFile(string path)
        {
            if (!File.Exists(path))
            {
                return new List<Line>();
            }

            var lines = File.ReadAllLines(path);

            return Parse(lines);
        }
    }
}
