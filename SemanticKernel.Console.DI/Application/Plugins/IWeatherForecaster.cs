using SemanticKernel.Console.DI.Domain;

namespace SemanticKernel.Console.DI.Application.Plugins;

public interface IWeatherForecaster : IKernelPlugin
{
    string GetTodaysWeather(string destination);
}