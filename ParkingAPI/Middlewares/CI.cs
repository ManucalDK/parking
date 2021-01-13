using ParkingAPI.Services;
using Microsoft.Extensions.DependencyInjection;
using ParkingAPI.Models;

namespace ParkingAPI.Middlewares
{
    // Control inversion
    public static class CI
    {
        public static IServiceCollection AddDependency(IServiceCollection services)
        {
            services.AddTransient<ICellService, CellService>();
            services.AddTransient<IEntryService, EntryService>();
            services.AddTransient<IDepartureService, DepartureService>();

            return services;
        }
    }
}
