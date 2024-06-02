namespace MoviesApi.Dtos
{
    public class MovieDetailsDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public int Year { get; set; }

        public double Rate { get; set; }

        public string Storeline { get; set; } = string.Empty;

        public byte[] Poster { get; set; }

        public byte GenreId { get; set; }

        public string GenreName { get; set; } = string.Empty;
    }
}
