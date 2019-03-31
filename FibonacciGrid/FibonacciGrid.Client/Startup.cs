using FibonacciGrid.Client.Services;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FibonacciGrid.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IFibonacciCheckerService, FibonacciCheckerService>();
            services.AddSingleton<IGridCellUpdaterService, GridCellUpdaterService>();
            services.AddSingleton<IFibonacciNeighborService, FibonacciNeighborService>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
