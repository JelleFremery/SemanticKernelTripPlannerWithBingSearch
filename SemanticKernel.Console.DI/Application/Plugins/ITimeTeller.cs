using SemanticKernel.Console.DI.Domain;

namespace SemanticKernel.Console.DI.Application.Plugins;

public interface ITimeTeller : IKernelPlugin
{
    string GetCurrentTime();
    bool IsOffPeak();
}