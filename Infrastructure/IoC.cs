using Application.Interfaces;
using Application.Services;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class IoC
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            services.AddDbContext<ParkingDbContext>(opt => opt.UseInMemoryDatabase("ParkingMemoryDb"));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IEntryService, EntryService>();
            services.AddTransient<ICellService, CellService>();
            services.AddTransient<IDepartureService, DepartureService>();
            services.AddTransient<IPlacaService, PlacaService>();
            services.AddTransient<IRateService, RateService>();

            return services;
        }
    }
}
