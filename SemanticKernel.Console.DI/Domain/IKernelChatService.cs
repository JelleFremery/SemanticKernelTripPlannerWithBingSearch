using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace SemanticKernel.Console.DI.Domain;

public interface IKernelChatService
{
    void ProcessAssistantReply(ChatHistory chat, string assistantOutput);

    IAsyncEnumerable<StreamingChatMessageContent> ProcessUserPrompt(ChatHistory chat, string userInput);
}
