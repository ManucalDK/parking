using ParkingAPI.Services;
using Microsoft.Extensions.DependencyInjection;
using ParkingAPI.Services.Interfaces;

namespace ParkingAPI.Middlewares
{
    // Control inversion
    public static class IoC
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
