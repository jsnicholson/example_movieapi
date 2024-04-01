using FunctionApp.Database.Models;
using FunctionApp.Models;
using System.Collections.Generic;

namespace FunctionApp.Endpoints.Models {
    public class MoviesResponse {
        public List<Movie> movies { get; set; }
        public Pagination pagination { get; set; }
    }
}
