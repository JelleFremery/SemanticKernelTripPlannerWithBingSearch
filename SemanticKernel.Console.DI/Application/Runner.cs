using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using SemanticKernel.Console.DI.Domain;
using ConsoleApp = System.Console;

namespace SemanticKernel.Console.DI.Application;

public class Runner(ILogger<Runner> logger, IKernelChatService kernelChatService)
{
    public async Task Start()
    {
        logger.LogInformation("Hello, World!");

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

        string? userInput;
        do
        {
            ConsoleApp.Write("User > ");
            userInput = ConsoleApp.ReadLine();
            if (userInput != null)
            {
                var result = kernelChatService.ProcessUserPrompt(chatMessages, userInput);

                ConsoleApp.Write("Assistant > ");
                // Stream the results
                StringBuilder fullMessage = new();
                await foreach (var content in result)
                {
                    ConsoleApp.Write(content.Content);
                    fullMessage.Append(content.Content);
                }

                kernelChatService.ProcessAssistantReply(chatMessages, fullMessage.ToString());
            }
            ConsoleApp.WriteLine("\n--------------------------------------------------------------");
        }
        while (userInput != null);
    }
}