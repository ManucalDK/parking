using AppCore.Entities;
using AppCore.Enums;
using AppCore.Exceptions;
using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingTest.Builders;
using System;
using System.Linq;

namespace ParkingTest.Integration.Services
{
    [TestClass()]
    public class IntegrationEntryServiceTest
    {
        private ParkingDbContext contexto;
        private IRepository<EntryEntity> entryRepository;
        private IRepository<CellEntity> cellRepository;
        private IRepository<DepartureEntity> departureRepository;
        private IRepository<RateEntity> rateRepository;
        private IRepository<PlacaEntity> placaRepository;
        private IRepository<PicoPlacaDigits> picoPlacarepository;

        EntryService entryService;
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
            entryService = new EntryService(entryRepository, cellService, departureService, placaService);
            contexto.Database.EnsureCreated();
        }

        [TestMethod(),ExpectedException(typeof(CellException))]
        public void EntryVehicle_WithoutCellAvaliable_ShouldReturnAnException()
        {
            // Arrange
            DtoEntry entryDTOBuilder = new EntryDTOBuilder().WithVehicleType(AppCore.Enums.VehicleTypeEnum.car).WithVehicleId("AAA111").Build();
            var response = "No hay cupos disponibles";


            cellService.DecreaseCell(VehicleTypeEnum.car, 20);
            // Act
            try
            {
                
                entryService.RegistryVehicle(entryDTOBuilder);
            }
            catch (Exception e)
            {
                // Assert
                Assert.AreEqual(e.Message, response);
                throw;
            }
        }

        [TestMethod()]
        public void EntryVehicle_ShouldReturn_CellWithAvaliableWith1Decrease()
        {
            // Arrange
            DtoEntry entryDTOBuilder = new EntryDTOBuilder()
                                    .WithVehicleType(VehicleTypeEnum.car)
                                    .WithVehicleId("AAA117")
                                    .Build();


            var cellsAvaliableBeforeEntry = cellRepository.List(cr => cr.IdVehicleType == VehicleTypeEnum.car).FirstOrDefault()?.NumCellAvaliable;

            // Act
            entryService.RegistryVehicle(entryDTOBuilder);
            var cellByVehicleType = cellRepository.List(cr => cr.IdVehicleType == VehicleTypeEnum.car).FirstOrDefault();

            // Assert
            Assert.IsTrue(cellByVehicleType.NumCellAvaliable == (cellsAvaliableBeforeEntry - 1));
        }
    }
}
