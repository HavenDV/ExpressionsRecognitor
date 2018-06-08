using System;
using RecognitionLibrary.Converters;

namespace RecognitorConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = args[0];

            Convert(path);
        }

        private static async void Convert(string path)
        {
            Console.WriteLine("Converting...");
            var actual = await ImageToLatexConverter.ConvertAsync(path);

            Console.WriteLine("Result:");
            Console.WriteLine(actual);
        }
    }
}
