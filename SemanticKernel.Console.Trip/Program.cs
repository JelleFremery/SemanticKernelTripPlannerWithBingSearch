using System.Text;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using SemanticKernel.Common;
using SemanticKernel.Console.Trip.Plugins;

var builder = Kernel.CreateBuilder();
builder.AddAzureOpenAIChatCompletion(
    deploymentName: Config.DeploymentName,
    endpoint: Config.ApiEndpoint,
    apiKey: Config.ApiKey);

builder.Plugins.AddFromType<TimeTeller>();
builder.Plugins.AddFromType<ElectricCar>();

builder.Plugins.AddFromType<WeatherForecaster>();
builder.Plugins.AddFromType<TripPlanner>();

var kernel = builder.Build();

IChatCompletionService chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

//Create a persona by using the system message
ChatHistory chatMessages = new(systemMessage: """
You are a friendly assistant who likes to follow the rules. You will complete required steps
and request approval before taking any consequential actions. If the user doesn't provide
enough information for you to complete a task, you will keep asking questions until you have
enough information to complete the task. 
""");

#if !DEBUG
chatMessages = new(systemMessage: """
        You are a friendly assistant who likes to follow the rules. You will complete required steps
        and request approval before taking any consequential actions. If the user doesn't provide
        enough information for you to complete a task, you will keep asking questions until you have
        enough information to complete the task. You speak in rhyme. 
        """);
#endif

while (true)
{
    Console.Write("User > ");
    string? userInput = Console.ReadLine();
    if (userInput == null)
    {
        Console.WriteLine("Please enter a prompt.");
    }
    else
    {
        chatMessages.AddUserMessage(userInput);

        OpenAIPromptExecutionSettings settings = new() { ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions };
        var result = chatCompletionService.GetStreamingChatMessageContentsAsync(
            chatMessages,
            executionSettings: settings,
            kernel: kernel);

        Console.Write("Assistant > ");
        // Stream the results
        StringBuilder fullMessage = new();
        await foreach (var content in result)
        {
            Console.Write(content.Content);
            fullMessage.Append(content.Content);
        }

        // Add the message from the agent to the chat history
        chatMessages.AddAssistantMessage(fullMessage.ToString());
    }
    Console.WriteLine("\n--------------------------------------------------------------");
}
