using System.ComponentModel;
using Microsoft.SemanticKernel;

namespace SemanticKernel.Console.Trip.Plugins;

#pragma warning disable S1118 
public class WeatherForecaster
#pragma warning restore S1118
{
    [KernelFunction]
    [Description("This function retrieves weather at given destination.")]
    [return: Description("Weather at given destination.")]
    public static string GetTodaysWeather([Description("The destination to retrieve the weather for.")] string destination)
    {
        // <--------- This is where you would call a fancy weather API to get the weather for the given <<destination>>.
        // We are just simulating a random weather here.
        string[] weatherPatterns = [$"Awful, as usual for {destination}", "Sunny", "Cloudy", "Windy", "Rainy", "Snowy"];
        Random rand = new();
        return weatherPatterns[rand.Next(weatherPatterns.Length)];
    }
}