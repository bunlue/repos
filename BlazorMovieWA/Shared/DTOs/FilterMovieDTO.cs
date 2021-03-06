﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorMovieWA.Shared.DTOs
{
    public class FilterMovieDTO
    {
        public int Page { get; set; } = 1;
        public int RecordPerPage { get; set; } = 10;
        public PaginationDTO Pagination 
        { 
            get { return new PaginationDTO() { Page = Page, RecordsPerPage = RecordPerPage }; }
        }
        public string Title { get; set; }
        public int GenreID { get; set; }
        public bool InTheaters { get; set; }
        public bool UpcomingReleases { get; set; }

    }
}
