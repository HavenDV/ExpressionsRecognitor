using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecognitionLibrary.Converters;
using RecognitionLibrary.Tests.Utilities;

namespace RecognitionLibrary.Tests
{
    [TestClass]
    public class ImageToLatexConverterTests
    {
        [TestMethod]
        public async Task ConvertTest()
        {
            await BaseConvertTest(@"\sqrt { t } = \frac { S } { v }", "1.jpg");
        }

        private static async Task BaseConvertTest(string expected, string name)
        {
            var path = TestUtilities.GetTestDataPath(name);
            var actual = await ImageToLatexConverter.ConvertAsync(path);

            Assert.AreEqual(expected, actual);
        }
    }
}
