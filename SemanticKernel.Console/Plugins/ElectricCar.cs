using System.ComponentModel;
using Microsoft.SemanticKernel;

namespace SemanticKernel.Console.Charge.Plugins;

public class ElectricCar
{
    private bool isCarCharging = false;

    [Description("This function starts charging the electric car.")]
    [KernelFunction]
    public string StartCharging()
    {
        if (isCarCharging)
        {
            return "Car is already charging.";
        }
        isCarCharging = true; // This is where one would call the MyVW API.
        return "Charging started.";
    }

    [Description("This function stops charging the electric car.")]
    [KernelFunction]
    public string StopCharging()
    {
        if (!isCarCharging)
        {
            return "Car is not charging.";
        }
        isCarCharging = false;
        return "Charging stopped.";
    }
}
