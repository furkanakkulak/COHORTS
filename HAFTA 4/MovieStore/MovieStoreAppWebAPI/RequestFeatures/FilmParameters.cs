namespace MovieStoreAppWebAPI.RequestFeatures
{
    public class FilmParameters : RequestParameters
    {
        public int GenreId { get; set; }
        public int DirectorId { get; set; }
        public List<int> PlayerIds { get; set; } = new List<int>();
        public DateTime? MinimumPublishedDate { get; set; }
        public DateTime? MaximumPublishedDate { get; set; }
    }
}
