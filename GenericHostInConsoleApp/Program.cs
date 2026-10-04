using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Reflection.Metadata.Ecma335;

namespace GenericHostInConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            //Add serilog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            //Generic Host
            using var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context,service) => {
                    // DI
                    service.AddTransient<IPrintRandomNumber, PrintRandomNumber>();
                
                })
                .Build();

            var svc = ActivatorUtilities.CreateInstance<PrintRandomNumber>(host.Services);
            svc.PrintNumber();
            svc.PrintNumber();
            svc.PrintNumber();
            svc.PrintNumber();
            svc.PrintNumber();


        }

        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();

        }
    }
}
