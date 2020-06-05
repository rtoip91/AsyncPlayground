using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AsyncPlayground.Interfaces;
using WindowCalculator.Interfaces;

namespace AsyncPlayground
{
    internal class Runner : IRunner
    {
        #region Private fields

        private readonly IFileHandler _fileHandler;

        private readonly IStringGenerator _stringGenerator;

        #endregion

        #region Ctors

        public Runner(IStringGenerator stringGenerator, IFileHandler fileHandler)
        {
            _stringGenerator = stringGenerator;
            _fileHandler = fileHandler;
        }

        #endregion

        #region IRunner Members

        public async Task Run()
        {
            string text = await _stringGenerator.Generate();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Text generate has been successful");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("How many times do you want to repeat generated text inside the file ?: ");
            string input = Console.ReadLine();
            bool parse = ulong.TryParse(input, out ulong amount);
            if (!parse)
            {
                throw new Exception("Wrong value !");
            }

            Console.WriteLine();
            Stopwatch stopwatch = Stopwatch.StartNew();
            await _fileHandler.WriteToFile(text, amount);
            long milliseconds = stopwatch.ElapsedMilliseconds;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Write to file has been finished ! It took {milliseconds} ms to finish");
            Console.WriteLine();

            stopwatch = Stopwatch.StartNew();
            await _fileHandler.WriteToFileWithSeparateTask(text, amount);
            milliseconds = stopwatch.ElapsedMilliseconds;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Write to file with tasks has been finished ! It took {milliseconds} ms to finish");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
        }

        #endregion
    }
}