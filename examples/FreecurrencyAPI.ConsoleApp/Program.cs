using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace FreecurrencyAPI.ConsoleApp;

static class Program
{
    static async Task Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console(theme: AnsiConsoleTheme.Code)
            .CreateLogger();

        await using var serviceProvider = RegisterServices(args);

        var worker = serviceProvider.GetRequiredService<Worker>();

        await worker.RunAsync(CancellationToken.None);
    }

    private static ServiceProvider RegisterServices(string[] args)
    {
        IConfiguration configuration = SetupConfiguration(args);
        var services = new ServiceCollection();

        services.AddSingleton(configuration);
        
        services.AddLogging(builder => builder.AddSerilog(logger: Log.Logger, dispose: true));

        // services.AddLogging(l => l.AddConsole());

        services.AddFreecurrencyAPI(configuration);
        
        services.AddSingleton<Worker>();

        return services.BuildServiceProvider();
    }

    private static IConfiguration SetupConfiguration(string[] args)
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();
    }
}