#nullable disable
namespace BLL.Services.Bases
{
    public abstract class GeminiServiceBase
    {
        // Member variables.
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }

        // Constructor.
        protected GeminiServiceBase()
        {
            DotNetEnv.Env.TraversePath().Load();
            var geminiKey = DotNetEnv.Env.LoadContents("GEMINI_API_KEY");
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