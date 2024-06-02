using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Services.Interfaces;


namespace MoviesApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("Api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreServices _genreServices;

        public GenresController(IGenreServices genreServices)
        {
            _genreServices = genreServices;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var genres = await _genreServices.GetAllAsync();
            return Ok(genres);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(GenreDto dto)
        {
            var genre = new Genre() { Name = dto.Name };
            genre = await _genreServices.CreateAsync(genre);
            return Ok(genre);
        }

        [HttpPut("{genreId}")]
        public async Task<IActionResult> UpdateAsync(byte genreId, [FromBody] GenreDto dto)
        {
            var genre = await _genreServices.GetByIdAsync(genreId);

            if (genre is null)
                return NotFound($"no genre was found with Id: {genreId}");

            genre.Name = dto.Name;
            genre = await _genreServices.UpdateAsync(genre);
            return Ok(genre);
        }

        [HttpDelete("{genreId}")]
        public async Task<IActionResult> DeleteAsync(byte genreId)
        {
            var genre = await _genreServices.GetByIdAsync(genreId);

            if (genre is null)
                return NotFound($"no genre was found with Id: {genreId}");

            genre = await _genreServices.DeleteAsync(genre);
            return Ok(genre);
        }
    }
}
