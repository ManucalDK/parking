using AppCore.Entities;
using AppCore.Enums;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Infrastructure.Data
{
    public class ParkingDbContext : DbContext, IApplicationDbContext
    {
        public ParkingDbContext(DbContextOptions<ParkingDbContext> options) : base(options)
        {

        }

        public DbSet<CellEntity> Cells { get; set; }
        public DbSet<EntryEntity> Entries { get; set; }
        public DbSet<DepartureEntity> Departures { get; set; }
        public DbSet<PlacaEntity> Placas { get; set; }
        public DbSet<PicoPlacaDigits> PicoPlaca { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            List<VehicleType> vehicleType = new List<VehicleType>
            {
                new VehicleType { Id = Guid.NewGuid().ToString(), InternalName = "Car", Description = "Car" },
                new VehicleType { Id = Guid.NewGuid().ToString(), InternalName = "Moto", Description = "Motorcycle" }
            };

            modelBuilder.Entity<VehicleType>().HasData(vehicleType.ToArray());

            List<CellEntity> cells = new List<CellEntity>
            {
                new CellEntity { Id = Guid.NewGuid().ToString(), IdVehicleType = VehicleTypeEnum.car, NumCellAvaliable = 20, NumTotalCells = 20 },
                new CellEntity { Id = Guid.NewGuid().ToString(), IdVehicleType = VehicleTypeEnum.motorcycle, NumCellAvaliable = 10, NumTotalCells = 10 }
            };

            modelBuilder.Entity<CellEntity>().HasData(cells.ToArray());

            List<RateEntity> rates = new List<RateEntity>
            {
                new RateEntity { Id = Guid.NewGuid().ToString(), IdVehicleType = VehicleTypeEnum.car, HourValue = 1000, DayValue = 8000,  DayChargeFrom = 9 },
                new RateEntity { Id = Guid.NewGuid().ToString(), IdVehicleType = VehicleTypeEnum.motorcycle, HourValue = 500, DayValue = 4000, DayChargeFrom = 9, SpecialChargeValue = 2000, SpecialChargeFromCC = 500}
            };

            modelBuilder.Entity<RateEntity>().HasData(rates.ToArray());

            var Id = Guid.NewGuid().ToString();
            var Id2 = Guid.NewGuid().ToString();

            List<PlacaEntity> placaEntities = new List<PlacaEntity>
            {
                new PlacaEntity { Id = Id, Type = VehicleTypeEnum.car, Length = 6 },
                new PlacaEntity { Id = Id2, Type = VehicleTypeEnum.motorcycle, Length = 6, LastNumberFrom = 2 }
            };

            modelBuilder.Entity<PlacaEntity>().HasData(placaEntities.ToArray());

            modelBuilder.Entity<PicoPlacaDigits>()
                                .HasOne(p => p.PlacaEntity)
                                .WithMany(b => b.PicoPlacaDigits);

            List<PicoPlacaDigits> picoPlaca = new List<PicoPlacaDigits>
            {
                new PicoPlacaDigits { Id = Guid.NewGuid().ToString(), Day= 1, Digit = 0, PlacaEntityID = Id},
                new PicoPlacaDigits { Id = Guid.NewGuid().ToString(),  Day= 1, Digit = 1, PlacaEntityID = Id},
                new PicoPlacaDigits { Id = Guid.NewGuid().ToString(), Day= 2, Digit = 2, PlacaEntityID = Id},
                new PicoPlacaDigits { Id = Guid.NewGuid().ToString(),  Day= 2, Digit = 3, PlacaEntityID = Id},
                new PicoPlacaDigits { Id = Guid.NewGuid().ToString(), Day= 3, Digit = 4, PlacaEntityID = Id},
                new PicoPlacaDigits { Id = Guid.NewGuid().ToString(),  Day= 3, Digit = 5, PlacaEntityID = Id},
                new PicoPlacaDigits { Id = Guid.NewGuid().ToString(), Day= 4, Digit = 6, PlacaEntityID = Id},
                new PicoPlacaDigits { Id = Guid.NewGuid().ToString(),  Day= 4, Digit = 7, PlacaEntityID = Id},
                new PicoPlacaDigits { Id = Guid.NewGuid().ToString(), Day= 5, Digit = 8, PlacaEntityID = Id},
                new PicoPlacaDigits { Id = Guid.NewGuid().ToString(),  Day= 5, Digit = 9, PlacaEntityID = Id},

                new PicoPlacaDigits { Id = Guid.NewGuid().ToString(), Day= 1, Digit = 0, PlacaEntityID = Id2},
                new PicoPlacaDigits { Id = Guid.NewGuid().ToString(),  Day= 1, Digit = 1, PlacaEntityID = Id2},
                new PicoPlacaDigits { Id = Guid.NewGuid().ToString(), Day= 2, Digit = 2, PlacaEntityID = Id2},
                new PicoPlacaDigits { Id = Guid.NewGuid().ToString(),  Day= 2, Digit = 3, PlacaEntityID = Id2},
                new PicoPlacaDigits { Id = Guid.NewGuid().ToString(), Day= 3, Digit = 4, PlacaEntityID = Id2},
                new PicoPlacaDigits { Id = Guid.NewGuid().ToString(),  Day= 3, Digit = 5, PlacaEntityID = Id2},
                new PicoPlacaDigits { Id = Guid.NewGuid().ToString(), Day= 4, Digit = 6, PlacaEntityID = Id2},
                new PicoPlacaDigits { Id = Guid.NewGuid().ToString(),  Day= 4, Digit = 7, PlacaEntityID = Id2},
                new PicoPlacaDigits { Id = Guid.NewGuid().ToString(), Day= 5, Digit = 8, PlacaEntityID = Id2},
                new PicoPlacaDigits { Id = Guid.NewGuid().ToString(),  Day= 5, Digit = 9, PlacaEntityID = Id2},
            };

            modelBuilder.Entity<PicoPlacaDigits>().HasData(picoPlaca.ToArray());

        }
    }
}
