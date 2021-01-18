using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using Application.Interfaces;
using AppCore.Entities;
using ParkingTest.Builders;
using Application.DTOs;
using AppCore.Exceptions;
using AppCore.Enums;
using System.Collections.Generic;

namespace Application.Services.Tests
{
    [TestClass()]
    public class EntryServiceTests
    {
        public Mock<IRepository<EntryEntity>> entryRepository;
        public Mock<IRepository<CellEntity>> cellRepository;
        public Mock<IRepository<DepartureEntity>> departureRepository;

        public Mock<IEntryService> entryService;
        public Mock<ICellService> _cellService;
        public Mock<IDepartureService> _departureService;
        public Mock<IPlacaService> _placaService;

        [TestInitialize]
        public void Setup()
        {
            // repositories
            entryRepository = new Mock<IRepository<EntryEntity>>();
            cellRepository = new Mock<IRepository<CellEntity>>();
            departureRepository = new Mock<IRepository<DepartureEntity>>();


            entryService = new Mock<IEntryService>();
            _cellService = new Mock<ICellService>();
            _departureService = new Mock<IDepartureService>();
            _placaService = new Mock<IPlacaService>();
        }

        [TestMethod()]
        public void RegisterMotorcycle_WithoutCC_ShouldReturnException()
        {
            // Arrange (preparación, organizar)
            var entryBuilder = new EntryDTOBuilder();
            DTOEntry entry = entryBuilder
                            .WithCC(null)
                            .Build();

            // cuando se ejecute este método en el entry service va a retornar true en la validacion de ExistsQuotaByVehicleType
            _cellService.Setup(cs => cs.ExistsQuotaByVehicleType(VehicleTypeEnum.motorcycle)).Returns(true);
            _placaService.Setup(ps => ps.GetLastNumberOfIdVehicle(entry.IdVehicleType, entry.IdVehicle)).Returns("5");

            var entryService = new EntryService(entryRepository.Object, _cellService.Object, _departureService.Object, _placaService.Object);
            

            // Act
            try
            {
                entryService.RegistryVehicle(entry);
            }
            catch (Exception e)
            {
                var message = "Falta la información del cilindraje de la motocicleta";
                // Assert (confirmacion)
                Assert.IsInstanceOfType(e, typeof(EntryException));
                Assert.AreEqual(e.Message, message);
            }
        }

        [TestMethod()]
        public void RegisterMotorcycle_WithoutCellAvaliable_ShouldReturnException()
        {
            // Arrange (preparación, organizar)
            var entryBuilder = new EntryDTOBuilder()
                .WithVehicleId("SFL55D")
                .WithVehicleType(VehicleTypeEnum.motorcycle)
                .WithCC("1000");

            _cellService.Setup(cs => cs.ExistsQuotaByVehicleType(VehicleTypeEnum.motorcycle)).Returns(false);


            var entryService = new EntryService(entryRepository.Object, _cellService.Object, _departureService.Object, _placaService.Object);
            DTOEntry entry = entryBuilder.Build();

            // Act
            try
            {
                entryService.RegistryVehicle(entry);
            }
            catch (Exception e)
            {
                var message = "No hay cupos disponibles";
                // Assert (confirmacion)
                Assert.IsInstanceOfType(e, typeof(CellException));
                Assert.AreEqual(e.Message, message);
            }
        }

        [TestMethod()]
        public void RegisterMotorcycle_WithPendingDeparture_ShouldReturnException()
        {
            // Arrange (preparación, organizar)
            var entryBuilder = new EntryDTOBuilder()
                .WithVehicleId("SFL55D")
                .WithVehicleType(VehicleTypeEnum.motorcycle)
                .WithCC("1000");
            var uniqueId = Guid.NewGuid().ToString();
            var entryList = new List<EntryEntity>();
           
            var entryEntity = new EntryEntityBuilder().Build();
            entryList.Add(entryEntity);
            var departureEntity = new DepartureEntityBuilder()
                                  .WithIdEntry(uniqueId)
                                  .Build();
            DTOEntry entry = entryBuilder.Build();

            entryRepository.Setup(er => er.List(e => e.IdVehicle == entry.IdVehicle)).Returns(entryList);

            var entryService = new EntryService(entryRepository.Object, _cellService.Object, _departureService.Object, _placaService.Object);
            

            // Act
            try
            {
                entryService.RegistryVehicle(entry);
            }
            catch (Exception e)
            {
                var message = "El vehículo que está registrando posee una salida pendiente";
                // Assert (confirmacion)
                Assert.IsInstanceOfType(e, typeof(EntryException));
                Assert.AreEqual(e.Message, message);
            }
        }

        [TestMethod()]
        public void RegisterMotorcycle_WithBadIdVehicleFormat_ShouldReturnException()
        {
            // Arrange (preparación, organizar)
            var entryBuilder = new EntryDTOBuilder()
                .WithVehicleId("SFL555")
                .WithVehicleType(VehicleTypeEnum.motorcycle)
                .WithCC("1000");

            DTOEntry entry = entryBuilder.Build();
            _cellService.Setup(cs => cs.ExistsQuotaByVehicleType(VehicleTypeEnum.motorcycle)).Returns(true);
            var entryService = new EntryService(entryRepository.Object, _cellService.Object, _departureService.Object, _placaService.Object);

            // Act
            try
            {
                entryService.RegistryVehicle(entry);
            }
            catch (Exception e)
            {
                var message = "Hubo un problema al leer la placa del vehículo. Verifique el tipo de vehículo e intente de nuevo";
                // Assert (confirmacion)
                Assert.IsInstanceOfType(e, typeof(EntryException));
                Assert.AreEqual(e.Message, message);
            }
        }
        
        [TestMethod()]
        public void RegisterMotorcycle_WithPicoPlaca_ShouldReturnException()
        {
            // Arrange (preparación, organizar)
            var entryBuilder = new EntryDTOBuilder()
                .WithVehicleId("SFL55D")
                .WithVehicleType(VehicleTypeEnum.motorcycle)
                .WithCC("1000");
            var day = 3;
            var placaLastNumber = 5;

            DTOEntry entry = entryBuilder.Build();
            EntryEntity entryEntity = new EntryEntityBuilder()
                                    .WithCC("1000")
                                    .Build();

            _cellService.Setup(cs => cs.ExistsQuotaByVehicleType(VehicleTypeEnum.motorcycle)).Returns(true);
            _placaService.Setup(ps => ps.GetLastNumberOfIdVehicle(VehicleTypeEnum.motorcycle, entryBuilder.IdVehicle)).Returns(placaLastNumber.ToString());
            _placaService.Setup(es => es.HasPicoPlaca(day, placaLastNumber)).Returns(true);
            entryRepository.Setup(er => er.Add(entryEntity)).Returns(entryEntity);

            var entryServiceClass = new EntryService(entryRepository.Object, _cellService.Object, _departureService.Object, _placaService.Object);

            // Act
            try
            {
                entryServiceClass.RegistryVehicle(entry);
            }
            catch (Exception e)
            {
                var message = "El vehículo no puede ser registrado, tiene pico y placa.";
                // Assert (confirmacion)
                Assert.IsInstanceOfType(e, typeof(EntryException));
                Assert.AreEqual(e.Message, message);
            }
        }
    }
}