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
    public class DetailsModel : PageModel
    {
        private readonly RazorPagesMovieBT.Data.RazorPagesMovieBTContext _context;

        public DetailsModel(RazorPagesMovieBT.Data.RazorPagesMovieBTContext context)
        {
            _context = context;
        }

        public Movie Movie { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie = await _context.Movie.FirstOrDefaultAsync(m => m.ID == id);

            if (Movie == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
