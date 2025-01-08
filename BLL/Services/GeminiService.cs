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
        
        // I implemented only this method as async because
        // the guy who implemented Gemini Dotnet sdk is implemented 
        // all SDK as async and it was not working synchronously.
        public async Task<GeminiServiceBase> SendGeminiRequest(string userMessage)
        {
            try
            {
                var response = await geminiClient.TextPrompt(userMessage);
                Console.WriteLine(response);
                // I know this is not a good approach but I'll keep it like this
                // just for now.
                var result = response.Candidates[0].Content.Parts[0].Text;
                return Success(result);
            }
            catch (Exception e)
            {
                Console.WriteLine("DOTNET EXCEPTION:" + e.Message);
                return Error(e.Message);
            }
        }
    }
}