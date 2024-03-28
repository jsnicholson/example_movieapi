﻿using System;

namespace FunctionApp.Models {
    public class Movie {
        public DateOnly Release_Date { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public float Popularity { get; set; }
        public int Vote_Count { get; set; }
        public float Vote_Average { get; set; }
        public string Original_Language { get; set; }
        public string Genre { get; set; }
        public string Poster_Url { get; set; }
    }
}