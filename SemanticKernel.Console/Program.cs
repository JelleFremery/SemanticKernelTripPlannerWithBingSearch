using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using SemanticKernel.Common;
using SemanticKernel.Console.Charge.Plugins;

var builder = Kernel.CreateBuilder();
builder.AddAzureOpenAIChatCompletion(
    deploymentName: Config.DeploymentName,
    endpoint: Config.ApiEndpoint,
    apiKey: Config.ApiKey
);

//Registering plugins
builder.Plugins.AddFromType<TimeTeller>();
builder.Plugins.AddFromType<ElectricCar>();

var kernel = builder.Build();

//Essential wiring!
OpenAIPromptExecutionSettings settings = new() { ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions };

while (true)
{
    Console.Write("User > ");
    string? userMessage = Console.ReadLine();
    if (userMessage == null)
    {
        Console.WriteLine("Please enter a prompt.");
    }
    else
    {
        Console.WriteLine(await kernel.InvokePromptAsync(userMessage, new(settings)));
    }
    Console.WriteLine("--------------------------------------------------------------");
}