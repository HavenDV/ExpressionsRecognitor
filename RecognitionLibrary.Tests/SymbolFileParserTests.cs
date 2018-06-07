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

            BaseLineTest(@"\sqrt", 10, 340, 401, 291, lines[0]);
            BaseLineTest(@"t", 200, 379, 130, 242, lines[1]);
            BaseLineTest(@"=", 410, 450, 221, 111, lines[2]);
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
            BaseLineTest(@"1", 285, 36, 61, 100, lines[2]);
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
            BaseLineTest(@"i", 120, 861, 61, 150, lines[2]);
            BaseLineTest(@"=", 191, 920, 131, 61, lines[3]);
            BaseLineTest(@"1", 330, 850, 67, 161, lines[4]);
            BaseLineTest(@"a", 520, 470, 191, 181, lines[5]);
            BaseLineTest(@"1", 720, 580, 71, 161, lines[6]);
            BaseLineTest(@"x", 830, 460, 191, 191, lines[7]);
            BaseLineTest(@"\delta", 1050, 300, 132, 171, lines[8]);
            BaseLineTest(@"1", 1170, 410, 48, 116, lines[9]);
            BaseLineTest(@"i", 1030, 590, 62, 153, lines[10]);
            BaseLineTest(@"x", 1280, 460, 192, 188, lines[11]);
            BaseLineTest(@"\delta", 1500, 269, 140, 170, lines[12]);
            BaseLineTest(@"j", 1620, 380, 81, 142, lines[13]);
            BaseLineTest(@"j", 1480, 590, 111, 201, lines[14]);
        }

        [TestMethod]
        public void ParseFileTest4()
        {
            var path = TestUtilities.GetTestDataPath("4.txt");

            var lines = SymbolFileParser.ParseFile(path);
            Assert.IsNotNull(lines);
            Assert.AreEqual(10, lines.Count);

            BaseLineTest(@"x", 25, 210, 75, 75, lines[0]);
            BaseLineTest(@"2", 100, 260, 40, 65, lines[1]);
            BaseLineTest(@"=", 205, 210, 102, 49, lines[2]);
            BaseLineTest(@"b", 320, 75, 75, 115, lines[3]);
            BaseLineTest(@"-", 425, 135, 47, 16, lines[4]);
            BaseLineTest(@"\sqrt", 530, 30, 235, 170, lines[5]);
            BaseLineTest(@"D", 635, 80, 116, 110, lines[6]);
            BaseLineTest(@"-", 308, 235, 472, 16, lines[7]);
            BaseLineTest(@"2", 470, 295, 68, 109, lines[8]);
            BaseLineTest(@"a", 545, 325, 75, 80, lines[9]);
        }

        [TestMethod]
        public void ParseFileTest5()
        {
            var path = TestUtilities.GetTestDataPath("5.txt");

            var lines = SymbolFileParser.ParseFile(path);
            Assert.IsNotNull(lines);
            Assert.AreEqual(6, lines.Count);

            BaseLineTest(@"e", 35, 60, 45, 55, lines[0]);
            BaseLineTest(@"-", 85, 50, 43, 10, lines[1]);
            BaseLineTest(@"\int", 145, 15, 35, 80, lines[2]);
            BaseLineTest(@"x", 195, 35, 43, 44, lines[3]);
            BaseLineTest(@"d", 239, 25, 42, 54, lines[4]);
            BaseLineTest(@"x", 280, 38, 43, 40, lines[5]);
        }

        [TestMethod]
        public void ParseFileTest6()
        {
            var path = TestUtilities.GetTestDataPath("6.txt");

            var lines = SymbolFileParser.ParseFile(path);
            Assert.IsNotNull(lines);
            Assert.AreEqual(7, lines.Count);

            BaseLineTest(@"a", 70, 150, 60, 60, lines[0]);
            BaseLineTest(@"+", 160, 140, 70, 70, lines[1]);
            BaseLineTest(@"b", 265, 35, 56, 80, lines[2]);
            BaseLineTest(@"-", 255, 125, 70, 10, lines[3]);
            BaseLineTest(@"c", 260, 150, 47, 60, lines[4]);
            BaseLineTest(@"-", 255, 225, 70, 8, lines[5]);
            BaseLineTest(@"d", 260, 245, 60, 79, lines[6]);
        }

        [TestMethod]
        public void ParseFileTest7()
        {
            var path = TestUtilities.GetTestDataPath("7.txt");

            var lines = SymbolFileParser.ParseFile(path);
            Assert.IsNotNull(lines);
            Assert.AreEqual(8, lines.Count);

            BaseLineTest(@"a", 30, 240, 90, 100, lines[0]);
            BaseLineTest(@"p", 135, 325, 63, 83, lines[1]);
            BaseLineTest(@"=", 285, 245, 115, 65, lines[2]);
            BaseLineTest(@"u", 430, 125, 105, 93, lines[3]);
            BaseLineTest(@"2", 545, 55, 52, 78, lines[4]);
            BaseLineTest(@"x", 600, 20, 61, 55, lines[5]);
            BaseLineTest(@"-", 410, 275, 230, 20, lines[6]);
            BaseLineTest(@"r", 490, 390, 71, 93, lines[7]);
        }

        [TestMethod]
        public void ParseFileTest8()
        {
            var path = TestUtilities.GetTestDataPath("8.txt");

            var lines = SymbolFileParser.ParseFile(path);
            Assert.IsNotNull(lines);
            Assert.AreEqual(21, lines.Count);

            BaseLineTest(@"(", 25, 50, 12, 35, lines[0]);
            BaseLineTest(@"x", 37, 60, 20, 20, lines[1]);
            BaseLineTest(@"-", 65, 65, 27, 5, lines[2]);
            BaseLineTest(@"c", 102, 60, 15, 19, lines[3]);
            BaseLineTest(@")", 117, 52, 13, 33, lines[4]);
            BaseLineTest(@"(", 137, 52, 13, 34, lines[5]);
            BaseLineTest(@"x", 150, 60, 20, 20, lines[6]);
            BaseLineTest(@"2", 167, 42, 13, 21, lines[7]);
            BaseLineTest(@"+", 190, 55, 25, 25, lines[8]);
            BaseLineTest(@"x", 225, 60, 19, 20, lines[9]);
            BaseLineTest(@"c", 245, 60, 14, 20, lines[10]);
            BaseLineTest(@"+", 270, 55, 23, 23, lines[11]);
            BaseLineTest(@"c", 303, 60, 16, 20, lines[12]);
            BaseLineTest(@"2", 317, 42, 14, 21, lines[13]);
            BaseLineTest(@")", 332, 52, 11, 33, lines[14]);
            BaseLineTest(@"=", 352, 61, 23, 11, lines[15]);
            BaseLineTest(@"x", 387, 60, 18, 20, lines[16]);
            BaseLineTest(@"3", 405, 42, 13, 21, lines[17]);
            BaseLineTest(@"-", 427, 65, 26, 5, lines[18]);
            BaseLineTest(@"c", 463, 60, 16, 21, lines[19]);
            BaseLineTest(@"3", 477, 42, 14, 20, lines[20]);
        }

        [TestMethod]
        public void ParseFileTest9()
        {
            var path = TestUtilities.GetTestDataPath("9.txt");

            var lines = SymbolFileParser.ParseFile(path);
            Assert.IsNotNull(lines);
            Assert.AreEqual(3, lines.Count);

            BaseLineTest(@"u", 20, 85, 90, 85, lines[0]);
            BaseLineTest(@"2", 120, 30, 55, 80, lines[1]);
            BaseLineTest(@"x", 185, 10, 60, 55, lines[2]);
        }

        [TestMethod]
        public void ParseFileTest10()
        {
            var path = TestUtilities.GetTestDataPath("10.txt");

            var lines = SymbolFileParser.ParseFile(path);
            Assert.IsNotNull(lines);
            Assert.AreEqual(3, lines.Count);

            BaseLineTest(@"x", 40, 50, 83, 81, lines[0]);
            BaseLineTest(@"y", 120, 110, 75, 85, lines[1]);
            BaseLineTest(@"2", 205, 67, 50, 64, lines[2]);
        }
    }
}
