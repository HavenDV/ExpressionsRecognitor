using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecognitionLibrary.Parsers;
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

        [TestMethod]
        public void ParseFileTest2()
        {
            var path = TestUtilities.GetTestDataPath("2.txt");
            
            var lines = SymbolFileParser.ParseFile(path);
            Assert.IsNotNull(lines);
            Assert.AreEqual(12, lines.Count);

            BaseLineTest(@"p", 124, 203, 35, 48, lines[0]);
            BaseLineTest(@"=", 159, 205, 49, 13, lines[1]);
            BaseLineTest(@"1", 190, 262, 29, 45, lines[2]);
            BaseLineTest(@"-", 191, 201, 10, 15, lines[3]);
            BaseLineTest(@"2", 193, 260, 30, 42, lines[4]);
            BaseLineTest(@"(", 200, 202, 10, 41, lines[5]);
            BaseLineTest(@"a", 208, 206, 25, 33, lines[6]);
            BaseLineTest(@"+", 212, 201, 19, 23, lines[7]);
            BaseLineTest(@"b", 219, 209, 22, 29, lines[8]);
            BaseLineTest(@"+", 224, 202, 18, 23, lines[9]);
            BaseLineTest(@"c", 231, 205, 20, 31, lines[10]);
            BaseLineTest(@")", 240, 203, 11, 40, lines[11]);
        }
    }
}
