using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Data;
using Microsoft.SemanticKernel.Plugins.Web.Bing;
using SemanticKernel.Common;
using SemanticKernel.Console.DI.Domain;

namespace SemanticKernel.Console.DI.Infrastructure.DependencyInjection;

public static class InfrastructureDependencies
{
    public static IServiceCollection RegisterInfrastructureDependencies(this IServiceCollection services, LogLevel logLevel)
    {
        var pluginTypes = Scanner();

        var pluginInterfaces = new List<Type>();

        pluginTypes.ForEach(type =>
        {
            var serviceType = type.GetInterfaces()
            .ToList()
            .Find(it => it.Name == $"I{type.Name}");

            if (serviceType == null) return;
            pluginInterfaces.Add(serviceType);
            services.AddSingleton(serviceType, type);
        });
        services.AddTransient<Kernel>(serviceProvider =>
        {
            var kernelBuilder = Kernel.CreateBuilder();
            kernelBuilder.Services.AddLogging(l => l
                .SetMinimumLevel(logLevel)
                .AddConsole()
            );

            pluginInterfaces.ForEach(type =>
            {
                kernelBuilder.Plugins.AddFromObject(serviceProvider.GetRequiredService(type));
            });

            // Let's add the built-in, experimental web search plugin for fun!            
#pragma warning disable SKEXP0050
            var textSearch = new BingTextSearch(apiKey: Config.BingKey);

            // Build a text search plugin with Bing search and add to the kernel
            var searchPlugin = textSearch.CreateWithSearch("SearchPlugin");
            kernelBuilder.Plugins.Add(searchPlugin);
#pragma warning restore SKEXP0050

            kernelBuilder.AddAzureOpenAIChatCompletion(
                deploymentName: Config.DeploymentName,
                endpoint: Config.ApiEndpoint,
                apiKey: Config.ApiKey);

            var kernel = kernelBuilder.Build();
            return kernel;
        });
        services.AddTransient<IKernelChatService, KernelChatService>();
        return services;
    }

    private static List<Type> Scanner()
    {
        var interfaceType = typeof(IKernelPlugin);
        // Get all loaded assemblies
        return Assembly.GetExecutingAssembly().GetTypes().Where(type =>
            type is { IsInterface: false, IsAbstract: false, IsClass: true } &&
            interfaceType.IsAssignableFrom(type)
        ).ToList();
    }
}
