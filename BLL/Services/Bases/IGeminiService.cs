namespace BLL.Services.Bases
{
    // You may ask why I did implement an interface
    // To be honest, for such a task that simple
    // It was not that necessary but I did it to understand
    // these interface and abstract class concepts better.
    // As far as I know, this SendGeminiRequest should be implemented when
    // a class inherits from this interface and its base abstract class.
    public interface IGeminiService
    {
        public interface IGeminiService
        {
            Task<GeminiServiceBase> SendGeminiRequest(string userMessage);
        }
    }
}