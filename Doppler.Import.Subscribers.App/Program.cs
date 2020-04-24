using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Doppler.Import.Subscribers.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = DefaultServiceCollection().BuildServiceProvider();

            var configuration = DefaultConfiguration().AddCommandLine(args).Build();

            Console.Out.WriteLine($"Environment: {configuration["ASPNETCORE_ENVIRONMENT"]}");
            if(args.Length >= 1) Console.Out.WriteLine($"  Command line:");
            foreach (var item in args)
            {
                Console.Out.WriteLine($"  {item}");
            }

            var importPtocess = serviceProvider.GetService<IProcess>();
            importPtocess.Execute();
        }

        static IConfigurationBuilder DefaultConfiguration() =>
            new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true)
                .AddEnvironmentVariables();

        static IServiceCollection DefaultServiceCollection() =>
            new ServiceCollection()
                .AddTransient<IProcess, Process>();
    }
}
