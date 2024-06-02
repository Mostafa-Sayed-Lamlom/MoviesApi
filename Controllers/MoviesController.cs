using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Services.Interfaces;

namespace MoviesApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("Api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieServices _movieServices;
        private readonly IGenreServices _genreServices;
        private readonly IMapper _mapper;
        private List<string> _allowedExtenstions = new List<string> { ".jpg", ".png" };
        private long _maxAllowedPosterSize = 1048576;
        public MoviesController(IMovieServices movieServices,
                                IGenreServices genreServices,
                                IMapper mapper)
        {
            _movieServices = movieServices;
            _genreServices = genreServices;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllAsyn()
        {
            var movies = await _movieServices.GetAllAsyn();
            var data = _mapper.Map<List<MovieDetailsDto>>(movies);
            return Ok(data);
        }

        [AllowAnonymous]
        [HttpGet("{movieId}")]
        public async Task<IActionResult> GetByIdAsync(int movieId)
        {
            var movie = await _movieServices.GetByIdAsync(movieId);
            if (movie == null)
                return NotFound($"no movie was found with Id: {movieId}");
            var data = _mapper.Map<MovieDetailsDto>(movie);
            return Ok(data);
        }

        [AllowAnonymous]
        [HttpGet("GetByGenreId/{genreId}")]
        public async Task<IActionResult> GetAllByGenreIdAsync(byte genreId)
        {
            var genre = await _genreServices.GetByIdAsync(genreId);
            if (genre == null)
                return NotFound($"no genre found with Id: {genreId}");

            var movies = await _movieServices.GetAllAsyn(genreId);

            var data = _mapper.Map<List<MovieDetailsDto>>(movies);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] MovieDto movieDto)
        {
            if (movieDto.Poster == null)
                return BadRequest("Poster is required!");

            if (!_allowedExtenstions.Contains(Path.GetExtension(movieDto.Poster.FileName).ToLower()))
                return BadRequest("Only .png and .jpg images are allowed!");

            if (movieDto.Poster.Length > _maxAllowedPosterSize)
                return BadRequest("Max allowed size for poster is 1MB!");

            var genre = await _genreServices.GetByIdAsync(movieDto.GenreId);

            if (genre == null)
                return BadRequest("Invalid genere ID!");

            using var stream = new MemoryStream();
            await movieDto.Poster.CopyToAsync(stream);

            var movie = _mapper.Map<Movie>(movieDto);
            movie.Poster = stream.ToArray();

            await _movieServices.CreateAsync(movie);
            var data = _mapper.Map<MovieDetailsDto>(movie);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(int movieId, [FromForm] MovieDto movieDto)
        {
            var movie = await _movieServices.GetByIdAsync(movieId);
            if (movie == null) return NotFound($"no movie was found with Id: {movieId}");

            var genre = await _genreServices.GetByIdAsync(movieDto.GenreId);

            if (genre == null)
                return BadRequest("Invalid genere ID!");

            if (movieDto.Poster != null)
            {
                if (!_allowedExtenstions.Contains(Path.GetExtension(movieDto.Poster.FileName).ToLower()))
                    return BadRequest("Only .png and .jpg images are allowed!");

                if (movieDto.Poster.Length > _maxAllowedPosterSize)
                    return BadRequest("Max allowed size for poster is 1MB!");

                var stream = new MemoryStream();
                await movieDto.Poster.CopyToAsync(stream);
                movie.Poster = stream.ToArray();
            }

            movie.Title = movieDto.Title;
            movie.Year = movieDto.Year;
            movie.Storeline = movieDto.Storeline;
            movie.Rate = movieDto.Rate;
            movie.GenreId = movieDto.GenreId;

            await _movieServices.UpdateAsync(movie);

            var data = _mapper.Map<MovieDetailsDto>(movie);
            return Ok(data);
        }

        [HttpDelete("{movieId}")]
        public async Task<IActionResult> DeleteAsync(int movieId)
        {
            var movie = await _movieServices.GetByIdAsync(movieId);
            if (movie == null) return NotFound($"no movie was found with id: {movieId}");
            await _movieServices.DeleteAsync(movie);

            var data = _mapper.Map<MovieDetailsDto>(movie);
            return Ok(data);
        }
    }
}