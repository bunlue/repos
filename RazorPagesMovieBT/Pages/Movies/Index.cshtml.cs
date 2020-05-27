using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovieBT.Data;
using RazorPagesMovieBT.Models;

namespace RazorPagesMovieBT.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovieBT.Data.RazorPagesMovieBTContext _context;

        public IndexModel(RazorPagesMovieBT.Data.RazorPagesMovieBTContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; }

        public async Task OnGetAsync()
        {
            Movie = await _context.Movie.ToListAsync();
        }
    }
}
