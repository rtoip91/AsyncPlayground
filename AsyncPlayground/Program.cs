using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WindowCalculator;

namespace AsyncPlayground
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            StringGenerator stringGenerator = new StringGenerator();
            FileHandler fileHandler = new FileHandler(@"D:\MyTest.txt",100000);

            try
            {
                string a = await stringGenerator.Generate();
                Console.WriteLine("Text generate has been sccesfull");
                Stopwatch stopwatch = Stopwatch.StartNew();
                await fileHandler.WriteToFile(a);
                long miliseconds = stopwatch.ElapsedMilliseconds;
                Console.WriteLine($"Write to file has been finished ! I took {miliseconds} ms to finish");
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
          
           


        }
    }
}
