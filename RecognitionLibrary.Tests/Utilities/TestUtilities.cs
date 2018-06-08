using System.IO;

namespace RecognitionLibrary.Tests.Utilities
{
    public static class TestUtilities
    {
        public static string TestImageDirectory = Path.Combine("..", "..", "..", "TestFiles");
        public static string GetTestDataPath(string name)
        {
            return Path.Combine(TestImageDirectory, name);
        }
    }
}
