using System;
using System.Runtime.InteropServices.JavaScript;
using BLL.Services.Bases;

namespace BLL.Services
{
    public class GeminiService : GeminiServiceBase, IGeminiService
    {
        // Constructor.
        public GeminiService()
        {
        }
        
        public GeminiServiceBase SendGeminiRequest(string userMessage)
        {
            var response =  geminiClient.TextPrompt(userMessage);
            Console.WriteLine(response);
            return this;
        }
    }
}