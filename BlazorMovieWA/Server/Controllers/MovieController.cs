using AutoMapper;
using BlazorMovieWA.Server.Helpers;
using BlazorMovieWA.Shared.DTOs;
using BlazorMovieWA.Shared.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovieWA.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]

    public class MovieController : ControllerBase

    {
        private readonly ApplicationDbContext context;
        private readonly IFileStorageService fileStorageService;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;
        private string containerName = "movies";

        public MovieController(ApplicationDbContext context,
            IFileStorageService fileStorageService,
            IMapper mapper,
            UserManager<IdentityUser> userManager
            )
        {
            this.context = context;
            this.fileStorageService = fileStorageService;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IndexPageDTO>> Get()
        {
            var limit = 6;

            var moviesInTheaters = await context.Movies
                .Where(x => x.InTheaters).Take(limit)
                .OrderByDescending(x => x.ReleaseDate)
                .ToListAsync();

            var todaysDate = DateTime.Today;

            var upcomingReleases = await context.Movies
                .Where(x => x.ReleaseDate > todaysDate)
                .Take(limit)
                .OrderBy(x => x.ReleaseDate)
                .ToListAsync();

            var response = new IndexPageDTO();
            response.InTheaters = moviesInTheaters;
            response.UpcomingReleases = upcomingReleases;

            return response;

        }

        [HttpGet("{id}")]
        [AllowAnonymous]

        public async Task<ActionResult<DetailsMovieDTO>> Get(int id)
        {
            var movie = await context.Movies.Where(x => x.Id == id)
                .Include(x => x.MovieGenre).ThenInclude(x => x.Genre)
                .Include(x => x.MovieActors).ThenInclude(x => x.Person)
                .FirstOrDefaultAsync();
            if (movie == null) { NotFound(); }

            var voteAverage = 0.0;
            var uservote = 0;

            if (await context.MovieRatings.AnyAsync(x=>x.MovieId == id))
            {
                voteAverage = await context.MovieRatings.Where(x => x.MovieId == id)
                    .AverageAsync(x => x.rate);

                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var user = await userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
                    var userId = user.Id;

                    var userVoteDB = await context.MovieRatings
                        .FirstOrDefaultAsync(x => x.MovieId == id && x.UserId == userId);

                    if (userVoteDB != null)
                    {
                        uservote = userVoteDB.rate;
                    }
                }

            }


            movie.MovieActors = movie.MovieActors.OrderBy(x => x.Order).ToList();

            var model = new DetailsMovieDTO();

            model.Movie = movie;
            model.Genres = movie.MovieGenre.Select(x => x.Genre).ToList();
            model.Actors = movie.MovieActors.Select(x =>
            new Person
            {
                Name = x.Person.Name,
                Picture = x.Person.Picture,
                Character = x.Character,
                Id = x.PersonId

            }).ToList();

            model.userVote = uservote;
            model.AverageVote = voteAverage;

            return model;

        }

        [HttpPost("filter")]
        [AllowAnonymous]

        public async Task<ActionResult<List<Movie>>> Filter(FilterMovieDTO filterMoviesDTO)
        {
            var movieQueryable = context.Movies.AsQueryable();
            if (!string.IsNullOrWhiteSpace(filterMoviesDTO.Title))
            {
                movieQueryable = movieQueryable
                      .Where(x => x.Title.Contains(filterMoviesDTO.Title));
            }
            if (filterMoviesDTO.InTheaters)
            {
                movieQueryable = movieQueryable.Where(x => x.InTheaters);
            }
            if (filterMoviesDTO.UpcomingReleases)
            {
                var today = DateTime.Today;
                movieQueryable = movieQueryable.Where(x => x.ReleaseDate > today);
            }
            if (filterMoviesDTO.GenreID != 0)
            {
                movieQueryable = movieQueryable
                    .Where(x => x.MovieGenre.Select(y => y.GenreId)
                    .Contains(filterMoviesDTO.GenreID));
            }
            await HttpContext.InsertPaginationParametersInResponse(movieQueryable,
                filterMoviesDTO.RecordPerPage);
            var movies = await movieQueryable.Paginate(filterMoviesDTO.Pagination).ToListAsync();

            return movies;
        }

        [HttpGet("Update/{id}")]
        public async Task<ActionResult<MovieUpdateDTO>> PutGet(int id)
        {
            var movieActionResult = await Get(id);
            if (movieActionResult.Result is NotFoundResult)
            {
                return NotFound();
            }
            var movieDetailDTO = movieActionResult.Value;
            var selectedGenresIds = movieDetailDTO.Genres.Select(x => x.ID).ToList();
            var notSelectedGenres = await context.Genres
                .Where(x => !selectedGenresIds.Contains(x.ID))
                .ToListAsync();

            var model = new MovieUpdateDTO();
            model.Movie = movieDetailDTO.Movie;
            model.SelectedGenres = movieDetailDTO.Genres;
            model.NotSelectedGenres = notSelectedGenres;
            model.Actors = movieDetailDTO.Actors;
            return model;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Movie movie)
        {
            if (!string.IsNullOrWhiteSpace(movie.Poster))
            {
                var poster = Convert.FromBase64String(movie.Poster);
                movie.Poster = await fileStorageService.SaveFile(poster, "jpg", containerName);
            }

            if (movie.MovieActors != null)
            {
                for (int i = 0; i < movie.MovieActors.Count; i++)
                {
                    movie.MovieActors[i].Order = i + 1;
                }
            }
            context.Add(movie);
            await context.SaveChangesAsync();
            return movie.Id;
        }

        [HttpPut]
        public async Task<ActionResult> Put(Movie movie)
        {
            var movieDB = await context.Movies.FirstOrDefaultAsync(x => x.Id == movie.Id);

            if (movieDB == null) { return NotFound(); }

            movieDB = mapper.Map(movie, movieDB);

            if (!string.IsNullOrWhiteSpace(movie.Poster))
            {
                var moviePoster = Convert.FromBase64String(movie.Poster);
                movieDB.Poster = await fileStorageService.EditFile(moviePoster,
                    "jpg", containerName, movieDB.Poster);
            }

            await context.Database.ExecuteSqlInterpolatedAsync($"delete from MovieActors where MovieId= {movie.Id}; delete from MovieGenres where MovieId= {movie.Id}");

            if (movie.MovieActors != null)
            {
                for (int i = 0; i < movie.MovieActors.Count; i++)
                {
                    movie.MovieActors[i].Order = i + 1;
                }
            }

            movieDB.MovieActors = movie.MovieActors;
            movieDB.MovieGenre = movie.MovieGenre;


            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var movie = await context.Movies.FirstOrDefaultAsync(x => x.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            context.Remove(movie);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
