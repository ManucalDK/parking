using AppCore.Entities;
using AppCore.Enums;
using AppCore.Exceptions;
using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ParkingTest.Builders;
using System;
using System.Collections.Generic;

namespace ParkingTest.Unit.Services
{


    [TestClass()]
    public class DepartureServiceTests
    {
        public Mock<IRepository<DepartureEntity>> _departureRepository;
        public Mock<IRepository<EntryEntity>> _entryRepository;
        public Mock<IRepository<RateEntity>> _rateRepository;
        public Mock<IDepartureService> _departureService;
        public Mock<IRateService> _rateService;
        public Mock<ICellService> _cellService;

        [TestInitialize]
        public void Setup()
        {
            // repositories
            _departureRepository = new Mock<IRepository<DepartureEntity>>();
            _entryRepository = new Mock<IRepository<EntryEntity>>();
            _rateRepository = new Mock<IRepository<RateEntity>>();

            // services
            _departureService = new Mock<IDepartureService>();
            _rateService = new Mock<IRateService>();
            _cellService = new Mock<ICellService>();
        }


        [TestMethod(), ExpectedException(typeof(DepartureException), "No arroja excepcion de tipo DepartureException")]
        public void RegisterDeparture_WithoutEntry_ShouldReturnAnException()
        {
            // Arrange (preparación)
            DTODeparture departureDTOBuilder = new DepartureDTOBuilder()
                                            .WithIdVehicle("AAA111")
                                            .Build();

            var entryService = new DepartureService(_departureRepository.Object, _entryRepository.Object, _rateService.Object, _cellService.Object);

            // Act
            entryService.RegistryDeparture(departureDTOBuilder);
        }

        [TestMethod(), ExpectedException(typeof(DepartureException), "No arroja excepcion de tipo DepartureException")]
        public void RegisterDeparture_WithoutRateConfiguration_ShouldReturnAnException()
        {
            // Arrange (preparación)
            var response = "No existe una tarifa configurada para el tipo de vehículo";
            var idVehicle = "AAA111";
            DTODeparture departureDTOBuilder = new DepartureDTOBuilder()
                                            .WithIdVehicle(idVehicle)
                                            .Build();
            var EntryEntity = new List<EntryEntity>();
            EntryEntity.Add(new EntryEntity() { IdVehicle = departureDTOBuilder.IdVehicle });

            _entryRepository.Setup(repo => repo.List(er => er.IdVehicle == idVehicle)).Returns(EntryEntity);
            //_rateService.Setup(service => service.GetRateByVehicleType(VehicleTypeEnum.car)).Returns(new RateEntity());

            var entryService = new DepartureService(_departureRepository.Object, _entryRepository.Object, _rateService.Object, _cellService.Object);

            // Act
            try
            {
                var result = entryService.RegistryDeparture(departureDTOBuilder);
            }
            catch (Exception e)
            {
                // Assert
                Assert.AreEqual(e.Message, response);
                throw;
            }
        }

        [TestMethod(), ExpectedException(typeof(DepartureException), "No arroja excepcion de tipo DepartureException")]
        public void RegisterDeparture_WithBadCCEntry_ShouldReturnAnException()
        {
            // Arrange (preparación)
            var response = "No fue posible determinar el cilindraje del vehículo";
            var IdVehicleType = VehicleTypeEnum.motorcycle;
            var idVehicle = "AAABBB";
            DTODeparture departure = new DepartureDTOBuilder()
                                            .WithIdVehicle(idVehicle)
                                            .Build();
            var EntryEntity = new List<EntryEntity>();
            EntryEntity.Add(new EntryEntity() { IdVehicle = departure.IdVehicle, IdVehicleType = IdVehicleType });

            _entryRepository.Setup(repo => repo.List(er => er.IdVehicle == idVehicle)).Returns(EntryEntity);
            _rateService.Setup(rs => rs.GetRateByVehicleType(IdVehicleType)).Returns(new RateEntity());

            var entryService = new DepartureService(_departureRepository.Object, _entryRepository.Object, _rateService.Object, _cellService.Object);

            // Act
            try
            {
                var result = entryService.RegistryDeparture(departure);
            }
            catch (Exception e)
            {
                // Assert
                Assert.AreEqual(e.Message, response);
                throw;
            }
        }
    }
}
