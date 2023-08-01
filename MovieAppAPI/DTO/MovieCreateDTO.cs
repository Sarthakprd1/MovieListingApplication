namespace MovieAppAPI.DTO
{
    public class MovieCreateDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int YearId { get; set; }
        public int GenreRefId { get; set; }
        public int CountryRefId { get; set; }
        public string MoviePhoto { get; set; }
    }
}
