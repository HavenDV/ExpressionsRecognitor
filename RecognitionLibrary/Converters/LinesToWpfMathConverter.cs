using System.Collections.Generic;
using System.Linq;
using RecognitionLibrary.Parsers;

namespace RecognitionLibrary.Converters
{
    public static class LinesToWpfMathConverter
    {
        public static string Convert(List<SymbolFileParser.Line> lines)
        {
            return string.Join(" ", lines.Select(line => line.Symbol));
        }
    }
}
