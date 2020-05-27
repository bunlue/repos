using BlazorMovieWA.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovieWA.Client.Repository
{
    public interface IRatingRepository
    {
        Task Vote(MovieRating movieRating);
    }
}
