using Microsoft.EntityFrameworkCore;
using Entities.Models;
using System.Collections.Generic;
using ParkingAPI.Services;

namespace ParkingAPI.Models
{
    public class ParkingContext: DbContext
    {
        public ParkingContext(DbContextOptions<ParkingContext> options): base(options){}

        public DbSet<Cell> Cells { get; set; }

        public DbSet<Departure> Departures { get; set; }

        public DbSet<Entry> Entries { get; set; }

        public DbSet<Rate> Rates { get; set; }

        public DbSet<VehicleType> VehicleTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            List<VehicleType> vehicleType = new List<VehicleType>
            {
                new VehicleType { Id = 1, InternalName = "Car", Description = "Car" },
                new VehicleType { Id = 2, InternalName = "Moto", Description = "Motorcycle" }
            };

            modelBuilder.Entity<VehicleType>().HasData(vehicleType.ToArray());

            List<Cell> cells = new List<Cell>
            {
                new Cell { Id = 1, IdVehicleType = (int)VehicleTypeEnum.car, NumCellAvaliable = 20 },
                new Cell { Id = 2, IdVehicleType = (int)VehicleTypeEnum.motorcycle, NumCellAvaliable = 10 }
            };

            modelBuilder.Entity<Cell>().HasData(cells.ToArray());

            List<Rate> rates = new List<Rate>
            {
                new Rate { Id = 1, IdVehicleType = (int)VehicleTypeEnum.car, HourValue = 1000, DayValue = 8000 },
                new Rate { Id = 2, IdVehicleType = (int)VehicleTypeEnum.motorcycle, HourValue = 500, DayValue = 4000 }
            };

            modelBuilder.Entity<Rate>().HasData(rates.ToArray());

        }
    }
}
