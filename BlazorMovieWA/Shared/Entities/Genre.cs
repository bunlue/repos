using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorMovieWA.Shared.Entities
{
    public class Genre
    {
        public int ID { get; set; }
        [Required(ErrorMessage ="ต้องมีข้อมูล")]
        public String Name { get; set; }
        public List<MovieGenre> MovieGenre { get; set; } = new List<MovieGenre>();

    }
}
