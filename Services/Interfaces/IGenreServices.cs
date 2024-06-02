namespace MoviesApi.Services.Interfaces
{
    public interface IGenreServices
    {
        Task<IEnumerable<Genre>> GetAllAsync();
        Task<Genre> CreateAsync(Genre genre);
        Task<Genre> GetByIdAsync(byte genreId);
        Task<Genre> UpdateAsync(Genre genre);
        Task<Genre> DeleteAsync(Genre genre);
    }
}
