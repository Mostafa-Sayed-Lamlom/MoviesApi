using Microsoft.EntityFrameworkCore;
using MoviesApi.Services.Interfaces;

namespace MoviesApi.Services.Implementations
{
    public class MovieServices : IMovieServices
    {
        private readonly ApplicationDbContext _context;

        public MovieServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Movie> CreateAsync(Movie movie)
        {
            await _context.AddAsync(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task<Movie> DeleteAsync(Movie movie)
        {
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task<IEnumerable<Movie>> GetAllAsyn(byte genreId = 0)
        {
            return await _context.Movies
                                 .Where(m => m.GenreId == genreId || genreId == 0)
                                 .OrderByDescending(m => m.Rate)
                                 .Include(m => m.Genre)
                                 .ToListAsync();
        }

        public async Task<Movie> GetByIdAsync(int movieId)
        {
            var movie = await _context.Movies
                                    .Where(m => m.Id == movieId)
                                    .Include(m => m.Genre)
                                    .FirstOrDefaultAsync();
            return movie!;
        }

        public async Task<Movie> UpdateAsync(Movie movie)
        {
            _context.Update(movie);
            await _context.SaveChangesAsync();
            return movie;
        }
    }
}
