using System.ComponentModel;
using SemanticKernel.Console.DI.Domain;

namespace SemanticKernel.Console.DI.Application.Plugins;

public interface IWeatherForecaster : IKernelPlugin
{
    string GetTodaysWeather([Description("The destination to retrieve the weather for.")] string destination);
}