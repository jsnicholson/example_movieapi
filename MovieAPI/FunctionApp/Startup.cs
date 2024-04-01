using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using FunctionApp;
using Microsoft.EntityFrameworkCore;
using System;
using FunctionApp.Database;

[assembly: FunctionsStartup(typeof(Startup))]
namespace FunctionApp {
    public class Startup : FunctionsStartup {
        public override void Configure(IFunctionsHostBuilder builder) {
            builder.Services.AddDbContext<MovieDbContext>(
                options => options.UseSqlite(Environment.GetEnvironmentVariable(Constants.CONNECTION_MOVIEDB)));
        }
    }
}