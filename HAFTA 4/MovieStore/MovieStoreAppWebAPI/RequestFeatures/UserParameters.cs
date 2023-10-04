namespace MovieStoreAppWebAPI.RequestFeatures
{
    public class UserParameters : RequestParameters
    {
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
    }
}
