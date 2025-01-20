using System.ComponentModel;
using Microsoft.SemanticKernel;
using SemanticKernel.Console.DI.Application.Plugins;

namespace SemanticKernel.Console.DI.Infrastructure.Plugins;

public class ElectricCar : IElectricCarPlugin
{
    private bool isCarCharging = false;
    private int batteryLevel = 0;
    private CancellationTokenSource? source;

    // Mimic charging the electric car, using a periodic timer.
    private async Task AddJuice()
    {
        source = new CancellationTokenSource();
        var timer = new PeriodicTimer(TimeSpan.FromSeconds(5));

        while (await timer.WaitForNextTickAsync(source.Token))
        {
            batteryLevel++;
            if (batteryLevel == 100)
            {
                isCarCharging = false;
                System.Console.WriteLine("\rBattery is full.");
                source.Cancel();
                return;
            }
            System.Console.Write("\rCharging {0}%", batteryLevel);
        }
    }

    [KernelFunction]
    [Description("This function checks if the electric car is currently charging.")]
    [return: Description("True if the car is charging; otherwise, false.")]
    public bool IsCarCharging() => isCarCharging;

    [KernelFunction]
    [Description("This function returns the current battery level of the electric car.")]
    [return: Description("The current battery level.")]
    public int GetBatteryLevel() => batteryLevel;


    [KernelFunction]
    [Description("This function starts charging the electric car.")]
    [return: Description("A message indicating the status of the charging process.")]
    public string StartCharging()
    {
        if (isCarCharging)
        {
            return "Car is already charging.";
        }
        else if (batteryLevel == 100)
        {
            return "Battery is already full.";
        }

        Task.Run(AddJuice);

        isCarCharging = true;
        return "Charging started.";
    }

    [KernelFunction]
    [Description("This function stops charging the electric car.")]
    [return: Description("A message indicating the status of the charging process.")]
    public string StopCharging()
    {
        if (!isCarCharging)
        {
            return "Car is not charging.";
        }
        isCarCharging = false;
        source?.Cancel();
        return "Charging stopped.";
    }
}