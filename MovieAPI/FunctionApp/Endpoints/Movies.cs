using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Linq;
using FunctionApp.Models;
using System;
using System.Linq.Dynamic;
using FunctionApp.Database.Models;
using FunctionApp.Factories;
using FunctionApp.Database;
using FunctionApp.Endpoints.Models;

namespace FunctionApp.Endpoints {
    public class Movies {
        private readonly MovieDbContext _context;

        public Movies(MovieDbContext db) {
            _context = db;
        }

        [FunctionName("movies")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log) {
            log.LogInformation("C# HTTP trigger function processed a request.");

            try {
                // get movies but keep queryable so EF doesnt pull them over the wire
                IQueryable<Movie> movies = _context.movies;

                string parameterTitle = req.Query[Constants.PARAMETER_TITLE];
                if(parameterTitle != null) {
                    movies = movies.Where(m => m.Title.ToLower().Contains(parameterTitle.ToLower()));
                }

                string parameterGenre = req.Query[Constants.PARAMETER_GENRE];
                if(parameterGenre != null) {
                    movies = movies.Where(m => m.Genre.ToLower().Contains(parameterGenre.ToLower()));
                }

                // get sort and check property exists
                string parameterSort = req.Query[Constants.PARAMETER_SORT];
                if(parameterSort != null) {
                    movies = movies.OrderBy($"{parameterSort} DESC");
                }

                // get pagination parameters and build Pagination object
                string parameterPage = req.Query[Constants.PARAMETER_PAGE];
                string parameterPageSize = req.Query[Constants.PARAMETER_PAGESIZE];
                Pagination pagination = Pagination.DefaultPagination;
                try {
                    pagination = PaginationFactory.CreatePagination(parameterPage, parameterPageSize, movies.Count());
                } catch (Exception exception) {
                    return new BadRequestObjectResult(new {
                        message = exception.Message
                    });
                }
                
                // apply pagination
                movies = movies
                    .Skip((pagination.page -1) * pagination.pageSize)
                    .Take(pagination.pageSize);

                return new OkObjectResult(new MoviesResponse() {
                    movies = movies.ToList(),
                    pagination = pagination
                });
            } catch (Exception exception) {
                return new ObjectResult(new {
                    message = exception.Message
                }) {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    }
}
