using System.ComponentModel;
using Microsoft.SemanticKernel;
using SemanticKernel.Console.DI.Application.Plugins;

namespace SemanticKernel.Console.DI.Infrastructure.Plugins;

public class TimeTeller : ITimeTeller
{
    [Description("This function retrieves the current time.")]
    [KernelFunction]
    public string GetCurrentTime() => DateTime.Now.ToString("F");

    [Description("This function checks if in off-peak period between 9pm and 7am")]
    [KernelFunction]
    public bool IsOffPeak() => DateTime.Now.Hour < 7 || DateTime.Now.Hour >= 21;
}
