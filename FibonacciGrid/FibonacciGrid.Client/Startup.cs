using FibonacciGrid.Client.Services;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FibonacciGrid.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IGridCellUpdaterService, GridCellUpdaterService>();
            services.AddSingleton<IFibonacciCheckerService, FibonacciCheckerService>();
            services.AddSingleton<IFibonacciNeighborService, FibonacciNeighborService>();
            services.AddSingleton<IFibonacciSequenceService, FibonacciSequenceService>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
