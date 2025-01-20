using Microsoft.SemanticKernel;
using SemanticKernel.Console.DI.Domain;

namespace SemanticKernel.Console.DI.Application.Plugins;

public interface ITripPlanner : IKernelPlugin
{
    Task<string> GenerateRequiredStepsAsync(Kernel kernel, string destination, string timeOfDay);
}