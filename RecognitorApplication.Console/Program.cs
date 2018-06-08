using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
            var result = Convert(path);

            if (needOutput)
            {
                var index1 = args.ToList().IndexOf("--output");
                var index2 = args.ToList().IndexOf("-o");
                var to = index1 >= 0 ? args[index1 + 1] : args[index2 + 1];

                File.WriteAllText(to, result);
            }
            else
            {
                Console.WriteLine("Result:");
                Console.WriteLine(result);
            }
        }

        private static string Convert(string path)
        {
            return ConvertAsync(path).GetAwaiter().GetResult();
        }

        private static async Task<string> ConvertAsync(string path)
        {
            return await ImageToLatexConverter.ConvertAsync(path);
        }
    }
}
