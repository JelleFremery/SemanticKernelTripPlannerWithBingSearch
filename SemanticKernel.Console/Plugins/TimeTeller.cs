using System.ComponentModel;
using Microsoft.SemanticKernel;

namespace SemanticKernel.Console.Charge.Plugins;

//Plugin classes cannot be static
#pragma warning disable S1118 // Utility classes should not have public constructors
public class TimeTeller
#pragma warning restore S1118 // Utility classes should not have public constructors
{
    [Description("This function retrieves the current time.")]
    [KernelFunction]
    public static string GetCurrentTime() => DateTime.Now.ToString("F");

    [Description("This function checks if in off-peak period between 9pm and 7am")]
    [KernelFunction]
    public static bool IsOffPeak() => DateTime.Now.Hour < 7 || DateTime.Now.Hour >= 21;
}
