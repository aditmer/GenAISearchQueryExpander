using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure.AI.OpenAI;
using OpenAI;
using System.Collections.Generic;
using Azure;
using OpenAI.Chat;

namespace Contoso.GenAISearch
{
    public static class QueryExpander
    {
        
        private static readonly string OpenAi4oMiniKey = Environment.GetEnvironmentVariable("OpenAi4oMiniKey");
        private static readonly string OpenAi4oMiniEndpoint = Environment.GetEnvironmentVariable("OpenAi4oMiniEndpoint");
        private static readonly string OpenAi4oEndpoint = Environment.GetEnvironmentVariable("OpenAi4oEndpoint");
        private static readonly string OpenAi4oKey = Environment.GetEnvironmentVariable("OpenAi4oKey");

        [FunctionName("QueryExpander")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log, ExecutionContext context)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string query = req.Query["query"];
            string model = req.Query["model"];

            if (string.IsNullOrEmpty(query))
            {
                return new BadRequestObjectResult("Please pass a query in the query string.");
            }

            AzureOpenAIClient azureClient;
            ChatClient chatClient;
            
            if(model == "gpt-4o") 
            {
                azureClient = new(new Uri(OpenAi4oEndpoint), new System.ClientModel.ApiKeyCredential(OpenAi4oKey));
                chatClient = azureClient.GetChatClient("gpt-4o");
            }
            else if (model == "gpt-4o-mini")
            {
                azureClient = new(new Uri(OpenAi4oMiniEndpoint), new System.ClientModel.ApiKeyCredential(OpenAi4oMiniKey));
                chatClient = azureClient.GetChatClient("gpt-4o-mini");
            }
            else
            {
                return new BadRequestObjectResult("Please pass a valid model in the query string. Valid values are gpt-4o and gpt-4o-mini.");
            }

            
            string promptPath = Path.Combine(context.FunctionAppDirectory, "Prompt.txt");
            string systemMessage = await File.ReadAllTextAsync(promptPath);

            ChatCompletion completion = chatClient.CompleteChat(new ChatMessage[]
            {
                new SystemChatMessage(systemMessage),
                new UserChatMessage(query)
            });

            
            // var completionOptions = new CompletionsOptions
            // {
            //     Prompts = new List<string> { query },
            //     MaxTokens = 1000
            // };

            // var response = await client.GetCompletionsAsync("gpt-4", completionOptions);
            // var completion = response.Choices[0].Text;

            return new OkObjectResult(completion);
        }
    }

    internal class CompletionsOptions
    {
        public List<string> Prompts { get; set; }
        public int MaxTokens { get; set; }
    }
}