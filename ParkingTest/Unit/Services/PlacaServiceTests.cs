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

        [TestInitialize]
        public void Setup()
        {
            // repos
            _placaRepository = new Mock<IRepository<PlacaEntity>>();

            // services
            _placaService = new Mock<IPlacaService>();
        }
        [TestMethod()]
        public void GetLastNumber_WithBadLength_ShouldReturnException()
        {
            // Arrange (preparación, organizar)
            var PlacaService = new PlacaService(_placaRepository.Object);
            int vehicleType = 2;
            string vehicleId = "SFL55";
            var placaEntity = new PlacaBuilder()
                                .WithLastNumberFrom(2)
                                .Build();
            var listRepository = new List<PlacaEntity>();
            listRepository.Add(placaEntity);

            _placaRepository.Setup(pr => pr.List(r => r.Type == PlacaType.motorcycle)).Returns(listRepository);

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
    }
}