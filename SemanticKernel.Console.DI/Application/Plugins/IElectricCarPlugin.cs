using SemanticKernel.Console.DI.Domain;

namespace SemanticKernel.Console.DI.Application.Plugins;

public interface IElectricCarPlugin : IKernelPlugin
{
    int GetBatteryLevel();
    bool IsCarCharging();
    string StartCharging();
    string StopCharging();
}