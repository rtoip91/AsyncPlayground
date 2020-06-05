using AsyncPlayground.Interfaces;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WindowCalculator.Interfaces;

namespace AsyncPlayground
{
    internal class Runner : IRunner
    {
       private IStringGenerator StringGenerator;
       private IFileHandler FileHandler;

        public Runner(IStringGenerator stringGenerator, IFileHandler fileHandler)
        {
            StringGenerator = stringGenerator;
            FileHandler = fileHandler;
        }
        public async Task Run ()
        {
            string text = await StringGenerator.Generate();
            Console.WriteLine("Text generate has been sccesfull");

            Stopwatch stopwatch = Stopwatch.StartNew();
            await FileHandler.WriteToFile(text);
            long miliseconds = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"Write to file has been finished ! It took {miliseconds} ms to finish");

            stopwatch = Stopwatch.StartNew();
            await FileHandler.WriteToFileWithSeparateTask(text);
            miliseconds = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"Write to file with tasks has been finished ! It took {miliseconds} ms to finish");

            Console.ReadKey();
        }
    }
}
