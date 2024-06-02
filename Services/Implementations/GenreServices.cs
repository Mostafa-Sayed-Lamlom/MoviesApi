using Microsoft.EntityFrameworkCore;
using MoviesApi.Services.Interfaces;

namespace MoviesApi.Services.Implementations
{
    public class GenreServices : IGenreServices
    {
        private readonly ApplicationDbContext _context;

        public GenreServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Genre> CreateAsync(Genre genre)
        {
            await _context.AddAsync(genre);
            await _context.SaveChangesAsync();
            return genre;
        }

        public async Task<Genre> DeleteAsync(Genre genre)
        {
            _context.Remove(genre);
            await _context.SaveChangesAsync();
            return genre;
        }

        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            var genres = await _context.Genres.OrderBy(g => g.Name).ToListAsync();
            return genres;
        }

        public async Task<Genre> GetByIdAsync(byte genreId)
        {
            var genre = await _context.Genres.SingleOrDefaultAsync(g => g.Id == genreId);
            return genre!;
        }

        public async Task<Genre> UpdateAsync(Genre genre)
        {
            _context.Update(genre);
            await _context.SaveChangesAsync();
            return genre;
        }
    }
}
