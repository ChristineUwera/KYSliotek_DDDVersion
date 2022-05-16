using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using static System.Reflection.Assembly;
using static System.Environment;
using Serilog;
using System.IO;

namespace KYSliotek
{
    public static class Program
    {
        static Program() =>
           CurrentDirectory = Path.GetDirectoryName(GetEntryAssembly().Location);

        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            var configuration = BuildConfiguration(args);

            ConfigureWebHostBuilder(configuration).Build().Run();
        }

        private static IConfiguration BuildConfiguration(string[] args)
        {
            return new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", false, false)
                    .Build();
        }

        public static IWebHostBuilder ConfigureWebHostBuilder(IConfiguration configuration)
        {
            return new WebHostBuilder()
                    .UseStartup<Startup>()
                    .UseConfiguration(configuration)
                    .UseContentRoot(CurrentDirectory)
                    .UseKestrel();
        }            
    }
}
