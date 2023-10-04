namespace MovieStoreAppWebAPI.Exceptions
{
    public class EntityNullException : Exception
    {
        public EntityNullException(string? message) : base(message)
        {
        }
    }
}
