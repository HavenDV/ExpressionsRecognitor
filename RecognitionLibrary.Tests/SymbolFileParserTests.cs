using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecognitionLibrary.Tests.Utilities;

namespace RecognitionLibrary.Tests
{
    [TestClass]
    public class SymbolFileParserTests
    {
        [TestMethod]
        public void ParseFileTest()
        {
            var path = TestUtilities.GetTestDataPath("1.txt");

            var lines = SymbolFileParser.ParseFile(path);
            Assert.IsNotNull(lines);
            Assert.AreEqual(6, lines.Count);

            BaseLineTest(@"\surd", 382, 230, 49, 33, lines[0]);
            BaseLineTest(@"t", 370, 220, 39, 23, lines[1]);
            BaseLineTest(@"=", 392, 209, 49, 13, lines[2]);
            BaseLineTest(@"S", 422, 290, 34, 45, lines[3]);
            BaseLineTest(@"-", 422, 240, 34, 5, lines[4]);
            BaseLineTest(@"u", 422, 200, 34, 22, lines[5]);
        }

        private static void BaseLineTest(string s, int x, int y, int w, int h, SymbolFileParser.Line actual)
        {
            Assert.AreEqual(s, actual.Symbol);
            Assert.AreEqual(x, actual.X);
            Assert.AreEqual(y, actual.Y);
            Assert.AreEqual(w, actual.Width);
            Assert.AreEqual(h, actual.Height);
        }
    }
}
