using AppCore.Entities;
using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingTest.Builders;
using System;

namespace ParkingTest.Integration.Services
{
    [TestClass()]
    public class IntegrationDepartureServiceTest
    {
        private ParkingDbContext contexto;
        private IRepository<EntryEntity> entryRepository;
        private IRepository<CellEntity> cellRepository;
        private IRepository<DepartureEntity> departureRepository;
        private IRepository<RateEntity> rateRepository;
        private IRepository<PlacaEntity> placaRepository;
        private IRepository<PicoPlacaDigits> picoPlacarepository;

        CellService cellService;
        DepartureService departureService;
        PlacaService placaService;
        RateService rateService;

        [TestInitialize]
        public void Setup()
        {

            var optionsBuilder = new DbContextOptionsBuilder<ParkingDbContext>().UseInMemoryDatabase("integrationTestDB");
            contexto = new ParkingDbContext(optionsBuilder.Options);
            entryRepository = new Repository<EntryEntity>(contexto);
            cellRepository = new Repository<CellEntity>(contexto);
            departureRepository = new Repository<DepartureEntity>(contexto);
            rateRepository = new Repository<RateEntity>(contexto);
            placaRepository = new Repository<PlacaEntity>(contexto);
            picoPlacarepository = new Repository<PicoPlacaDigits>(contexto);

            cellService = new CellService(cellRepository);
            rateService = new RateService(rateRepository);
            placaService = new PlacaService(placaRepository, picoPlacarepository);

            departureService = new DepartureService(departureRepository, entryRepository, rateService, cellService);
            contexto.Database.EnsureCreated();
        }

        [TestMethod()]
        public void DepartureOfCar_With1DayAnd3Hours_ShouldReturnAValueToPayOf11000()
        {
            // Arrange
            var idVehicle = "SFL555";
            DtoDeparture entryDTOBuilder = new DepartureDTOBuilder().WithIdVehicle(idVehicle).Build();
            var valueToPay = 11000;
            entryRepository.Add(new EntryEntity()
            {
                EntryTime = DateTime.Now.Subtract(TimeSpan.FromHours(27).Subtract(TimeSpan.FromSeconds(5))),
                IdVehicle = idVehicle,
                IdVehicleType = AppCore.Enums.VehicleTypeEnum.car,
                Id = Guid.NewGuid().ToString()
            });

            // Act
            cellService.DecreaseCell(AppCore.Enums.VehicleTypeEnum.car, 1);
            var response = departureService.RegistryDeparture(entryDTOBuilder);

            // Assert
            Assert.AreEqual(response.RateValue, valueToPay);
        }

        [TestMethod()]
        public void DepartureOfMotorcycle_With650CCAnd10HoursOfParking_ShouldReturnAValueToPayOf6000()
        {
            // Arrange
            var idVehicle = "SFL555";
            DtoDeparture entryDTOBuilder = new DepartureDTOBuilder().WithIdVehicle(idVehicle).Build();
            var valueToPay = 6000;
            entryRepository.Add(new EntryEntity()
            {
                EntryTime = DateTime.Now.Subtract(TimeSpan.FromHours(10).Subtract(TimeSpan.FromSeconds(5))),
                IdVehicle = idVehicle,
                IdVehicleType = AppCore.Enums.VehicleTypeEnum.motorcycle,
                Id = Guid.NewGuid().ToString(),
                CC = "650"
            });

            cellService.DecreaseCell(AppCore.Enums.VehicleTypeEnum.motorcycle, 1);
            // Act
            var response = departureService.RegistryDeparture(entryDTOBuilder);

            // Assert
            Assert.AreEqual(response.RateValue, valueToPay);
        }
    }
}
