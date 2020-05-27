using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovieBT.Models;

namespace RazorPagesMovieBT.Data
{
    public class RazorPagesMovieBTContext : DbContext
    {
        public RazorPagesMovieBTContext (DbContextOptions<RazorPagesMovieBTContext> options)
            : base(options)
        {
        }

        public DbSet<RazorPagesMovieBT.Models.Movie> Movie { get; set; }
    }
}
