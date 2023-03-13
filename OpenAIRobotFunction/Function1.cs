using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using OpenAI_API.Completions;
using OpenAI_API.Models;

namespace OpenAIRobotFunction
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;

        public Function1(ILogger<Function1> log)
        {
            _logger = log;
        }


        [FunctionName("wordMeaning")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "OpenAI" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "word", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **word** parameter")]
        [OpenApiParameter(name: "answer", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "The **answer** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req)
        {
            try
            {
                string word = req.Query["word"];
                string answer = req.Query["answer"];
                var api = new OpenAI_API.OpenAIAPI(apiKeys: APIKeys.OpenAIKey);


                if (answer == null)
                {
                    var result = await api.Completions.CreateCompletionAsync(
                    new CompletionRequest($"What's the meaning of the word \"{word}\"?",
                    model: Model.DavinciText,
                    temperature: 0.3,
                    max_tokens: 60,
                    top_p: 1,
                    frequencyPenalty: 0,
                    presencePenalty: 0));

                    return new OkObjectResult(result.ToString());
                }
                else
                {
                    var result = await api.Completions.CreateCompletionAsync(
                    new CompletionRequest($"Is the word \"{word}\", meaning: {answer}?",
                    model: Model.DavinciText,
                    temperature: 0.3,
                    max_tokens: 60,
                    top_p: 1,
                    frequencyPenalty: 0,
                    presencePenalty: 0));

                    return new OkObjectResult(result.ToString());
                }
                

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetWorkdMeaning");

                return new OkObjectResult("Failed to connect to OpenAI");
            }
            
        }

        [FunctionName("wordAntonyms")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "OpenAI" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "word", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **word** parameter")]
        [OpenApiParameter(name: "answer", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "The **answer** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run4(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req)
        {
            try
            {
                string word = req.Query["word"];
                string answer = req.Query["answer"];
                var api = new OpenAI_API.OpenAIAPI(apiKeys: APIKeys.OpenAIKey);


                if (answer == null)
                {
                    var result = await api.Completions.CreateCompletionAsync(
                    new CompletionRequest($"What's are the antonyms of the word \"{word}\"?",
                    model: Model.DavinciText,
                    temperature: 0.3,
                    max_tokens: 60,
                    top_p: 1,
                    frequencyPenalty: 0,
                    presencePenalty: 0));
                    return new OkObjectResult(result.ToString());
                }
                else
                {
                    var result = await api.Completions.CreateCompletionAsync(
                        new CompletionRequest($"Is the word \"{word}\", an antonyms of: {answer}?",
                        model: Model.DavinciText,
                        temperature: 0.3,
                        max_tokens: 60,
                        top_p: 1,
                        frequencyPenalty: 0,
                        presencePenalty: 0));

                    return new OkObjectResult(result.ToString());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "getWordAntonyms");

                return new OkObjectResult("Failed to connect to OpenAI");
            }

        }


        [FunctionName("wordSynonyms")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "OpenAI" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "word", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **word** parameter")]
        [OpenApiParameter(name: "answer", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "The **answer** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run3(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req)
        {
            try
            {
                string word = req.Query["word"];
                string answer = req.Query["answer"];
                var api = new OpenAI_API.OpenAIAPI(apiKeys: APIKeys.OpenAIKey);

                if (answer == null)
                {
                    var result = await api.Completions.CreateCompletionAsync(
                    new CompletionRequest($"What's are the synonyms of the word \"{word}\"?",
                    model: Model.DavinciText,
                    temperature: 0.7,
                    max_tokens: 256,
                    top_p: 1,
                    frequencyPenalty: 0,
                    presencePenalty: 0));
                    return new OkObjectResult(result.ToString());
                }
                else
                {
                    var result = await api.Completions.CreateCompletionAsync(
                        new CompletionRequest($"Is the word \"{word}\", a synonyms of: {answer}?",
                        model: Model.DavinciText,
                        temperature: 0.7,
                        max_tokens: 256,
                        top_p: 1,
                        frequencyPenalty: 0,
                        presencePenalty: 0));

                    return new OkObjectResult(result.ToString());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "getWordSynonyms");

                return new OkObjectResult("Failed to connect to OpenAI");
            }

        }
    }
}

