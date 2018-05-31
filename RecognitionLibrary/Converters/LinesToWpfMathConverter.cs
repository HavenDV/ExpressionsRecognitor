using System;
using System.Collections.Generic;
using System.Linq;
using RecognitionLibrary.Parsers;

namespace RecognitionLibrary.Converters
{
    public static class LinesToWpfMathConverter
    {
        public static string Convert(List<Line> lines)
        {
            var normalized = NormalizeLines(lines);
            var x = -1;
            var delta = 100 / lines.Count;
            var groups = new List<List<Line>>();
            var currentGroup = new List<Line>();
            foreach (var line in normalized)
            {
                if (!currentGroup.Any() || line.CenterX - x < delta)
                {
                    if (x == -1)
                    {
                        x = line.CenterX;
                    }
                    currentGroup.Add(line);
                    continue;
                }

                groups.Add(currentGroup);
                currentGroup = new List<Line> {line};
                x = line.CenterX;
            }

            if (currentGroup.Any())
            {
                groups.Add(currentGroup);
            }

            return string.Join(" ", groups.Select(ConvertGroup));
        }

        private static string ConvertGroup(List<Line> lines)
        {
            if (lines.Any(line => line.Symbol == "-"))
            {
                var index = lines.FindIndex(line => line.Symbol == "-");

                return $@"\frac{{{ConvertGroup(lines.Take(index).ToList())}}}{{{ConvertGroup(lines.Skip(index + 1).ToList())}}}";
            }

            if (lines.Any(line => line.Symbol == @"\surd"))
            {

                return $@"\sqrt{{{ConvertGroup(lines.Skip(1).ToList())}}}";
            }

            if (lines.Any(line => line.Symbol == @"\sum"))
            {
                var index = lines.FindIndex(line => line.Symbol == @"\sum");

                return $@"\sum ^{{{ConvertGroup(lines.Take(index).ToList())}}} _{{{ConvertGroup(lines.Skip(index + 1).ToList())}}}";// Скорее всего нужно поправить
            }

            return string.Join(" ", lines.Select(line => line.Symbol));
        }

        private static List<Line> NormalizeLines(List<Line> lines)
        {
            var max = Math.Max(lines.Max(i => i.X + i.Width), lines.Max(i => i.Y + i.Height));

            return lines.Select(line => new Line
            {
                Symbol = line.Symbol,
                X = (int) Math.Round(100.0 * line.X / max),
                Y = (int) Math.Round(100.0 * line.Y / max),
                Width = (int) Math.Round(100.0 * line.Width / max),
                Height = (int) Math.Round(100.0 * line.Height / max)
            }).ToList();
        }
    }
}
