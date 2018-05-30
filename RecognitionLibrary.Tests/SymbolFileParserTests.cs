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

            BaseLineTest(@"\surd", 10, 340, 401, 291, lines[0]);
            BaseLineTest(@"t", 200, 379, 130, 242, lines[1]);
            BaseLineTest(@"=", 440, 449, 222, 113, lines[2]);
            BaseLineTest(@"S", 739, 99, 213, 283, lines[3]);
            BaseLineTest(@"-", 709, 490, 292, 42, lines[4]);
            BaseLineTest(@"u", 750, 640, 211, 222, lines[5]);
        }

        private static void BaseLineTest(string s, int x, int y, int w, int h, Line actual)
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

            BaseLineTest(@"p", 25, 150, 86, 106, lines[0]);
            BaseLineTest(@"=", 160, 155, 91, 52, lines[1]);
            BaseLineTest(@"1", 185, 36, 61, 100, lines[2]);
            BaseLineTest(@"-", 265, 175, 95, 20, lines[3]);
            BaseLineTest(@"2", 280, 235, 66, 100, lines[4]);
            BaseLineTest(@"(", 375, 120, 45, 136, lines[5]);
            BaseLineTest(@"a", 425, 150, 66, 76, lines[6]);
            BaseLineTest(@"+", 530, 145, 71, 71, lines[7]);
            BaseLineTest(@"b", 625, 120, 71, 106, lines[8]);
            BaseLineTest(@"+", 730, 145, 71, 71, lines[9]);
            BaseLineTest(@"c", 830, 150, 63, 76, lines[10]);
            BaseLineTest(@")", 895, 120, 42, 136, lines[11]);
        }

        [TestMethod]
        public void ParseFileTest3()
        {
            var path = TestUtilities.GetTestDataPath("3.txt");

            var lines = SymbolFileParser.ParseFile(path);
            Assert.IsNotNull(lines);
            Assert.AreEqual(15, lines.Count);

            BaseLineTest(@"n", 200, 140, 112, 122, lines[0]);
            BaseLineTest(@"\sum", 60, 290, 400, 510, lines[1]);
            BaseLineTest(@"i", 120, 861, 61, 100, lines[2]);
            BaseLineTest(@"=", 191, 920, 131, 61, lines[3]);
            BaseLineTest(@"1", 330, 850, 67, 161, lines[4]);
            BaseLineTest(@"a", 520, 470, 191, 181, lines[5]);
            BaseLineTest(@"1", 720, 580, 71, 161, lines[6]);
            BaseLineTest(@"x", 830, 460, 191, 191, lines[7]);
            BaseLineTest(@"\delta", 1050, 300, 132, 171, lines[8]);
            BaseLineTest(@"1", 1170, 410, 48, 116, lines[9]);
            BaseLineTest(@"i", 130, 590, 62, 153, lines[10]);
            BaseLineTest(@"x", 1280, 460, 192, 188, lines[11]);
            BaseLineTest(@"\delta", 1500, 269, 140, 170, lines[12]);
            BaseLineTest(@"j", 1620, 380, 81, 142, lines[13]);
            BaseLineTest(@"j", 1480, 590, 111, 201, lines[14]);
        }
    }
}
