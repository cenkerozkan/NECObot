#nullable disable
using DotnetGeminiSDK;
using DotnetGeminiSDK.Client;
using DotnetGeminiSDK.Config;

namespace BLL.Services.Bases
{
    public abstract class GeminiServiceBase
    {
        // Member variables.
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        // https://github.com/gsilvamartin/dotnet-gemini-sdk
        public readonly GeminiClient geminiClient;

        // Constructor.
        protected GeminiServiceBase()
        {
            // Seems like I'm gonna have a little bit of trouble
            // with getting env variables in MacOS :D
            // EDIT: No I was wrong, I forgot to call GetString method
            //       called LoadContents instead of. Now it works :D
            DotNetEnv.Env.TraversePath().Load();
            var geminiKey = DotNetEnv.Env.GetString("GEMINI_API_KEY");
            // https://github.com/gsilvamartin/dotnet-gemini-sdk
            // I'm only using gemini in here to make a more presentable 
            // project, This is why im instantiating it in the base class.
            // I'll remove it later when we start to use python service for 
            // the chatbot.
            geminiClient = new GeminiClient(new GoogleGeminiConfig(){ApiKey = geminiKey});
        }

        public GeminiServiceBase Success(string message = "")
        {
            IsSuccessful = true;
            Message = message;
            return this;
        }

        public GeminiServiceBase Error(string message = "")
        {
            IsSuccessful = false;
            Message = message;
            return this;
        }
    }
}