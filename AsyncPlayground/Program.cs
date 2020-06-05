using AsyncPlayground.Interfaces;
using Autofac;
using System;
using System.Threading.Tasks;
using WindowCalculator;
using WindowCalculator.Interfaces;

namespace AsyncPlayground
{
    class Program
    {
        private static IContainer Container { get; set; }
      
        static async Task Main(string[] args)
        {
            RegisterContainer();
            try
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    IRunner runner = scope.Resolve<IRunner>();
                    await runner.Run();
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
        private static void RegisterContainer()
        {
            var builder = new ContainerBuilder();
            builder.Register(r => new FileHandler(@"D:\MyTest.txt", 700000)).As<IFileHandler>();
            builder.RegisterType<StringGenerator>().As<IStringGenerator>();
            builder.RegisterType<Runner>().As<IRunner>();
            Container = builder.Build();
        }
    }
}
