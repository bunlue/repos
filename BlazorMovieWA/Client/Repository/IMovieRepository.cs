using BlazorMovieWA.Shared.DTOs;
using BlazorMovieWA.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovieWA.Client.Repository
{
    public interface IMovieRepository
    {
        Task<int> CreateMovie(Movie movie);
        Task DeleteMovie(int Id);
        Task<DetailsMovieDTO> GetDetailsMovieDTO(int id);
        Task<IndexPageDTO> GetIndexPageDTO();
        Task<MovieUpdateDTO> GetMovieForUpdate(int id);
        Task<PaginatedResponse<List<Movie>>> GetMoviesFiltered(FilterMovieDTO filterMoviesDTO);
        Task UpdateMovie(Movie movie);
    }
}
