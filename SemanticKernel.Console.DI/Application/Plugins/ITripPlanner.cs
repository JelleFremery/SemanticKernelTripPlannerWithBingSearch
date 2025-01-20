using System.ComponentModel;
using Microsoft.SemanticKernel;
using SemanticKernel.Console.DI.Domain;

namespace SemanticKernel.Console.DI.Application.Plugins;

public interface ITripPlanner : IKernelPlugin
{
    Task<string> GenerateRequiredStepsAsync(Kernel kernel, [Description("A 2-3 sentence description of where is a good place to go to today")] string destination, [Description("The time of the day to start the trip")] string timeOfDay);
}