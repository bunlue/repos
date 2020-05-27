using BlazorMovieWA.Client.Helpers;
using BlazorMovieWA.Shared.DTOs;
using BlazorMovieWA.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovieWA.Client.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly IHttpService httpService;
        private string url = "api/movie";
        public MovieRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<IndexPageDTO> GetIndexPageDTO()
        {

            return await httpService.GetHelper<IndexPageDTO>(url);

        }

        public async Task<MovieUpdateDTO> GetMovieForUpdate(int id)
        {
            return await httpService.GetHelper<MovieUpdateDTO>($"{url}/update/{id}");
        }
        public async Task<DetailsMovieDTO> GetDetailsMovieDTO(int id)
        {
           return await httpService.GetHelper<DetailsMovieDTO>($"{url}/{id}");
        }

        public async Task<PaginatedResponse<List<Movie>>> GetMoviesFiltered( FilterMovieDTO filterMoviesDTO)
        {
            var responseHTTP = await httpService.Post<FilterMovieDTO, List<Movie>>($"{url}/filter", filterMoviesDTO);
            var totalAmountPages = int.Parse(responseHTTP.HttpResponseMessage.Headers.GetValues("totalAmountPages").FirstOrDefault());
            var paginatedResponse = new PaginatedResponse<List<Movie>>()
            {
                Response = responseHTTP.Response,
                TotalAmountPages = totalAmountPages
            };
            return paginatedResponse;

        }

        public async Task<int> CreateMovie(Movie movie)
        {
            var response = await httpService.Post<Movie, int>(url, movie);
            if (!response.Success)
            {

                throw new ApplicationException(await response.GetBody());

            }
            return response.Response;
        }

        public async Task UpdateMovie(Movie movie)
        {
            var response = await httpService.Put(url, movie);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task DeleteMovie(int Id)
        {
            var response = await httpService.Delete($"{url}/{Id}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());

            }
        }
    }
}
