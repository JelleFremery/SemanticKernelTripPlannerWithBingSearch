using System.ComponentModel;
using Microsoft.SemanticKernel;

namespace SemanticKernel.Console.Trip.Plugins;

#pragma warning disable S1118 
public class TimeTeller
#pragma warning restore S1118 
{
    [Description("This function retrieves the current time.")]
    [KernelFunction]
    public static string GetCurrentTime() => DateTime.Now.ToString("F");

    [Description("This function checks if in off-peak period between 9pm and 7am")]
    [KernelFunction]
    public static bool IsOffPeak() => DateTime.Now.Hour < 7 || DateTime.Now.Hour >= 21;
}
