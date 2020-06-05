using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AsyncPlayground.Interfaces;
using FileGenerator.Interfaces;

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

            IList<Task> tasks = new List<Task>();
            tasks.Add(First(text, amount));
            tasks.Add(Second(text, amount));
            await Task.WhenAll(tasks);

            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
        }

        private async Task Second(string text, ulong amount)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Second started");
            Stopwatch stopwatch = Stopwatch.StartNew();
            await _fileHandler.WriteToFileWithSeparateTask(text, amount);
            long milliseconds = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"Write to file with tasks has been finished ! It took {milliseconds} ms to finish");
        }

        private async Task First(string text, ulong amount)
        { 
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("First started");
            Stopwatch stopwatch = Stopwatch.StartNew();
            await _fileHandler.WriteToFile(text, amount);
            long milliseconds = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"Write to file has been finished ! It took {milliseconds} ms to finish");
            Console.WriteLine();
        }

        #endregion
    }
}