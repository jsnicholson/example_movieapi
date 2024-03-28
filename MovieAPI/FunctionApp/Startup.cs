using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using FunctionApp;

[assembly: FunctionsStartup(typeof(Startup))]
namespace FunctionApp {
    public class Startup : FunctionsStartup {
        public override void Configure(IFunctionsHostBuilder builder) {
        }
    }
}
