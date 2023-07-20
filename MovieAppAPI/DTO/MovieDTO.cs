namespace MovieAppAPI.DTO
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int YearId { get; set; }
        public int GenreRefId { get; set; }
        public int CountryRefId { get; set; }
    }
}
