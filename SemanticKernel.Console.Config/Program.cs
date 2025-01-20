using Microsoft.SemanticKernel;
using SemanticKernel.Common;

var builder = Kernel.CreateBuilder();
builder.AddAzureOpenAIChatCompletion(
    deploymentName: Config.DeploymentName,
    endpoint: Config.ApiEndpoint,
    apiKey: Config.ApiKey
);

var kernel = builder.Build();

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
        Console.WriteLine(await kernel.InvokePromptAsync(userMessage));
    }
    Console.WriteLine("--------------------------------------------------------------");
}