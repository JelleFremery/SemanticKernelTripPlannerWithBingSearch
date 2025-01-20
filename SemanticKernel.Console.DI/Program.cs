using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SemanticKernel.Console.DI.Application;
using SemanticKernel.Console.DI.Infrastructure.DependencyInjection;

var services = CreateServices(LogLevel.Information);
Runner runner = services.GetRequiredService<Runner>();
await runner.Start();

ServiceProvider CreateServices(LogLevel logLevel)
{
    var serviceProvider = new ServiceCollection()
        .AddLogging(options =>
        {
            options.ClearProviders();
            options.SetMinimumLevel(logLevel);
            options.AddConsole();
        })
        .RegisterInfrastructureDependencies(logLevel)
        .AddSingleton<Runner>()
        .BuildServiceProvider();

    return serviceProvider;
}
