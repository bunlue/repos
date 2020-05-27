using BlazorMovieWA.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorMovieWA.Shared.DTOs
{
    public class IndexPageDTO
    {
        public List<Movie> InTheaters { get; set; }
        public List<Movie> UpcomingReleases { get; set; }
    }
}
