using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecognitionLibrary.Converters;
using RecognitionLibrary.Parsers;
using RecognitionLibrary.Tests.Utilities;

namespace RecognitionLibrary.Tests
{
    [TestClass]
    public class LinesToWpfMathConverterTests
    {
        [TestMethod]
        public void ConvertTest()
        {
            BaseConvertTest(@"\sqrt{t} = \frac{S}{u}", "1.txt");
            BaseConvertTest(@"p = \frac{1}{2} ( a + b + c )", "2.txt");
        }

        private static void BaseConvertTest(string expected, string name)
        {
            var path = TestUtilities.GetTestDataPath(name);
            var lines = SymbolFileParser.ParseFile(path);
            var actual = LinesToWpfMathConverter.Convert(lines);

            Assert.AreEqual(expected, actual);
        }
    }
}
