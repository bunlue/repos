using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorMovieWA.Shared.Entities
{
    public class MovieActors
    {
        public int PersonId { get; set; }
        public int MovieId { get; set; }
        public Person Person { get; set; }
        public Movie Movie { get; set; }
        public string Character { get; set; }
        public int Order { get; set; }

    }
}
