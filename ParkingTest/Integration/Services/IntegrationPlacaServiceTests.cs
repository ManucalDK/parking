using AppCore.Entities;
using AppCore.Enums;
using Application.Interfaces;
using Application.Services;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParkingTest.Integration.Services
{
    [TestClass]
    public class IntegrationPlacaServiceTests
    {

        private ParkingDbContext contexto;
        private IRepository<PlacaEntity> _placaRepository;
        IRepository<PicoPlacaDigits> _picoPlacarepository;
        PlacaService placaService;

        [TestInitialize]
        public void Setup()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ParkingDbContext>().UseInMemoryDatabase("integrationTestDB");
            contexto = new ParkingDbContext(optionsBuilder.Options);
            _placaRepository = new Repository<PlacaEntity>(contexto);
            _picoPlacarepository = new Repository<PicoPlacaDigits>(contexto);
            placaService = new PlacaService(_placaRepository, _picoPlacarepository);
            contexto.Database.EnsureCreated();
        }

        [TestMethod]
        public void HasPicoPlaca_ShouldReturnTrue_WhenExistsVehicleWithIt()
        {
            // Arrange
            var vehicleType = VehicleTypeEnum.car;
            var day = 1;
            var lastNumberVehicleId = 1;

            // Act
            var result = placaService.HasPicoPlaca(vehicleType, day, lastNumberVehicleId);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void HasPicoPlaca_ShouldReturnFalse_WhenVehicleHasNotPicoPlaca()
        {
            // Arrange
            var vehicleType = VehicleTypeEnum.car;
            var day = 5;
            var lastNumberVehicleId = 1;

            // Act
            var result = placaService.HasPicoPlaca(vehicleType, day, lastNumberVehicleId);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
