using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;
                // get pagination parameters and build Pagination object
                string parameterPage = req.Query[Constants.PARAMETER_PAGE];
                string parameterPageSize = req.Query[Constants.PARAMETER_PAGESIZE];
                Pagination pagination = Pagination.DefaultPagination;
                try {
                    pagination = PaginationFactory.CreatePagination(parameterPage, parameterPageSize, movies.Count());
                } catch (ArgumentException exception) {
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
