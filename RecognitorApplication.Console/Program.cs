using System;
using System.IO;
using System.Linq;
using RecognitionLibrary.Converters;

namespace RecognitorConsoleApplication
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class Program
    {
        private static void Main(string[] args)
        {
            var needOutput = args.Contains("--output") || args.Contains("-o");
            var path = args.LastOrDefault();

            Console.WriteLine("Converting...");
            var result = ImageToLatexConverter.ConvertAsync(path).GetAwaiter().GetResult();

            if (needOutput)
            {
                var index1 = args.ToList().IndexOf("--output");
                var index2 = args.ToList().IndexOf("-o");
                var to = index1 >= 0 ? args[index1 + 1] : args[index2 + 1];

                File.WriteAllText(to, result);

                Console.WriteLine("Done.");
            }
            else
            {
                Console.WriteLine("Result:");
                Console.WriteLine(result);
            }
        }
    }
}
