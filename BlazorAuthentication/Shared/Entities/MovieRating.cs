using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorMovieWA.Shared.Entities
{
    public class MovieRating
    {
            public int id { get; set; }
            public int rate { get; set; }
            public DateTime RatingDate { get; set; }
            public int MovieId { get; set; }
            public string UserId { get; set; }
    }
}
