using System;
using System.Threading.Tasks;
using AsyncPlayground.Interfaces;
using Autofac;
using FileGenerator;
using FileGenerator.Interfaces;

namespace AsyncPlayground
{
    internal class Program
    {
        #region Properties

        private static IContainer Container { get; set; }

        #endregion

        #region Private methods

        private static async Task Main(string[] args)
        {
            RegisterContainer();
            try
            {
                await using ILifetimeScope scope = Container.BeginLifetimeScope();
                IRunner runner = scope.Resolve<IRunner>();
                await runner.Run();
            }

            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(e.Message);
            }
        }

        private static void RegisterContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.Register(r => new FileHandler(@"C:\MyTest.txt")).As<IFileHandler>();
            builder.RegisterType<StringGenerator>().As<IStringGenerator>();
            builder.RegisterType<Runner>().As<IRunner>();
            Container = builder.Build();
        }

        #endregion
    }
}