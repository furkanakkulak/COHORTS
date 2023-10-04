namespace MovieStoreAppWebAPI.Services.Logging
{
    public class ConsoleLogger : ILoggerService
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}
