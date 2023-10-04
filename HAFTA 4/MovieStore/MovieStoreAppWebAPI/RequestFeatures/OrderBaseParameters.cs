namespace MovieStoreAppWebAPI.RequestFeatures
{
    public class OrderBaseParameters
    {
        public string? FilmName { get; set; }
        public DateTime? MinimumOrderedDate { get; set; }
        public DateTime? MaximumOrderedDate { get; set; }
    }
}
