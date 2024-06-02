namespace MoviesApi.Services.Interfaces
{
    public interface IMovieServices
    {
        Task<IEnumerable<Movie>> GetAllAsyn(byte genreId = 0);
        Task<Movie> GetByIdAsync(int movieId);
        Task<Movie> CreateAsync(Movie movie);
        Task<Movie> UpdateAsync(Movie movie);
        Task<Movie> DeleteAsync(Movie movie);
    }
}
