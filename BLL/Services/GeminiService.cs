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
            // This is a dummy implementation.
            return Error("This is a dummy implementation.");
        }
    }
}