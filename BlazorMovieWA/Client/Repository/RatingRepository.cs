using BlazorMovieWA.Client.Helpers;
using BlazorMovieWA.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovieWA.Client.Repository 

{
    public class RatingRepository: IRatingRepository
    {
        private readonly IHttpService httpService;
        private readonly string urlBase = "api/rating";

        public RatingRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task Vote(MovieRating movieRating)
        {
            Console.WriteLine($"Movie Rating {movieRating.rate}");
            var httpResponse = await httpService.Post(urlBase, movieRating);
            if (!httpResponse.Success)
            {
                throw new ApplicationException(await httpResponse.GetBody());

            }
        }
    }
}
