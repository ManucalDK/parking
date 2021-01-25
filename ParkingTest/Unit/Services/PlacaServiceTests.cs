using Microsoft.VisualStudio.TestTools.UnitTesting;
using Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Application.Interfaces;
using Moq;
using AppCore.Entities;
using ParkingTest.Builders;
using AppCore.Enums;
using System.Linq;
using AppCore.Exceptions;

namespace Application.Services.Tests
{
    [TestClass()]
    public class PlacaServiceTests
    {
        public Mock<IPlacaService> _placaService;


        public Mock<IRepository<PlacaEntity>> _placaRepository;
        private Mock<IRepository<PicoPlacaDigits>> _picoPlacarepository;

        [TestInitialize]
        public void Setup()
        {
            // repos
            _placaRepository = new Mock<IRepository<PlacaEntity>>();
            _picoPlacarepository = new Mock<IRepository<PicoPlacaDigits>>();

            // services
            _placaService = new Mock<IPlacaService>();
        }

        [TestMethod()]
        public void GetLastNumber_WithBadLength_ShouldReturnException()
        {
            // Arrange (preparación, organizar)
            var PlacaService = new PlacaService(_placaRepository.Object, _picoPlacarepository.Object);
            VehicleTypeEnum vehicleType = VehicleTypeEnum.motorcycle;
            string vehicleId = "SFL55";
            var placaEntity = new PlacaBuilder()
                                .WithLastNumberFrom(2)
                                .Build();
            var listRepository = new List<PlacaEntity>();
            listRepository.Add(placaEntity);

            _placaRepository.Setup(pr => pr.List(r => r.Type == VehicleTypeEnum.motorcycle)).Returns(listRepository);

            // Act
            try
            {
                PlacaService.GetLastNumberOfIdVehicle(vehicleType, vehicleId);
            }
            catch (Exception e)
            {
                var message = "La placa del vehículo posee un problema en el formato. La longitud debe ser de 6";
                // Assert (confirmacion)
                Assert.IsInstanceOfType(e, typeof(PlacaException));
                Assert.AreEqual(e.Message, message);
            }
        }

        [TestMethod()]
        public void HasPicoPlaca_ShouldReturnTrue_WhenExistsPicoPlaca()
        {
            // Arrange
            var vehicleTypeId = VehicleTypeEnum.car;
            var placaEntity = new PlacaBuilder().Build();
            var result = placaEntity;
            int day = 5;
            int vehicleLastNumberId = 9;
            var listPicoPlaca = new List<PicoPlacaDigits>
            {
                new PicoPlacaDigits
                {
                    Id = Guid.NewGuid().ToString(),
                    Day = 5,
                    Digit = 8,
                    PlacaEntityID = result.Id
                },
                new PicoPlacaDigits
                {
                    Id = Guid.NewGuid().ToString(),
                    Day = 5,
                    Digit = 9,
                    PlacaEntityID = result.Id
                },
            };

            _placaRepository.Setup(setup => setup.List(repo => repo.Type == vehicleTypeId)).Returns(new List<PlacaEntity> { placaEntity });
            _picoPlacarepository.Setup(setup => setup.List(repo => repo.PlacaEntityID == result.Id && repo.Day == day && repo.Digit == vehicleLastNumberId)).Returns(listPicoPlaca);

            var placaService = new PlacaService(_placaRepository.Object, _picoPlacarepository.Object);

            var response = placaService.HasPicoPlaca(vehicleTypeId, day, vehicleLastNumberId);

            Assert.IsTrue(response);
        }

        [TestMethod()]
        public void HasPicoPlaca_ShouldReturnFalse_WhenExistsPicoPlaca()
        {
            // Arrange
            var vehicleTypeId = VehicleTypeEnum.car;
            var placaEntity = new PlacaBuilder().Build();
            var result = placaEntity;
            int day = 5;
            int vehicleLastNumberId = 9;
            var listPicoPlaca = new List<PicoPlacaDigits>();

            _placaRepository.Setup(setup => setup.List(repo => repo.Type == vehicleTypeId)).Returns(new List<PlacaEntity> { placaEntity });
            _picoPlacarepository.Setup(setup => setup.List(repo => repo.PlacaEntityID == result.Id && repo.Day == day && repo.Digit == vehicleLastNumberId)).Returns(listPicoPlaca);

            var placaService = new PlacaService(_placaRepository.Object, _picoPlacarepository.Object);

            var response = placaService.HasPicoPlaca(vehicleTypeId, day, vehicleLastNumberId);

            Assert.IsFalse(response);
        }
    }
}