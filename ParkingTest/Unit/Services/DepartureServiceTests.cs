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
using System.Linq;

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

        [TestMethod()]
        public void GetDepartures_WithData_ShouldReturnListWithValues()
        {
            // Arrange
            _departureRepository.Setup(r => r.List()).Returns(new List<DepartureEntity> {
                new DepartureEntity
                {
                    DepartureTime = DateTime.Now,
                    Id = Guid.NewGuid().ToString()
                }
            });
            var entryService = new DepartureService(_departureRepository.Object, _entryRepository.Object, _rateService.Object, _cellService.Object);


            // Act
            var result = entryService.GetDepartures();

            // Assert
            Assert.IsTrue(result.Count() > 0);
        }

        [TestMethod()]
        public void GetDepartures_WithoutData_ShouldReturnEmptyList()
        {
            // Arrange
            _departureRepository.Setup(r => r.List()).Returns(new List<DepartureEntity>());
            var entryService = new DepartureService(_departureRepository.Object, _entryRepository.Object, _rateService.Object, _cellService.Object);


            // Act
            var result = entryService.GetDepartures();

            // Assert
            Assert.IsTrue(result.Count() <= 0);
        }

        [TestMethod()]
        public void GetEntryById_ShouldReturn_DtoDepartureEntity()
        {
            // Arrange
            DepartureEntity departureEntity = new DepartureEntityBuilder()
                                                .WithId(Guid.NewGuid().ToString())
                                                .Build();

            _departureRepository.Setup(r => r.GetById(departureEntity.Id)).Returns(departureEntity);

            var entryService = new DepartureService(_departureRepository.Object, _entryRepository.Object, _rateService.Object, _cellService.Object);

            // Act
            DTODeparture result =  entryService.GetEntryById(id: departureEntity.Id);

            // Assert
            Assert.IsNotNull(result);

        }

        [TestMethod()]
        public void GetEntryByBadId_ShouldReturn_DtoEmpty()
        {
            // Arrange
            var newId = Guid.NewGuid().ToString();
            _departureRepository.Setup(r => r.GetById(newId));

            var entryService = new DepartureService(_departureRepository.Object, _entryRepository.Object, _rateService.Object, _cellService.Object);

            // Act
            DTODeparture result = entryService.GetEntryById(id: newId);

            // Assert
            Assert.IsNull(result.Id);
        }

        [TestMethod()]
        public void GetDepartureByEntryId_ShouldReturn_DtoDepartureEntity()
        {
            // Arrange
            DepartureEntity departureEntity = new DepartureEntityBuilder()
                                                .WithId(Guid.NewGuid().ToString())
                                                .Build();

            var id = departureEntity.Id;

            _departureRepository.Setup(r => r.List(dr => dr.IdEntry == id)).Returns(new List<DepartureEntity> { departureEntity });

            var entryService = new DepartureService(_departureRepository.Object, _entryRepository.Object, _rateService.Object, _cellService.Object);

            // Act
            DepartureEntity result = entryService.GetDepartureByEntryId(id);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void GetEntryByBadId_ShouldReturn_Null()
        {
            // Arrange
            DepartureEntity departureEntity = new DepartureEntityBuilder()
                                                .WithId(Guid.NewGuid().ToString())
                                                .Build();

            var id = Guid.NewGuid().ToString();

            _departureRepository.Setup(r => r.List(dr => dr.IdEntry == id)).Returns(new List<DepartureEntity>());

            var entryService = new DepartureService(_departureRepository.Object, _entryRepository.Object, _rateService.Object, _cellService.Object);

            // Act
            DepartureEntity result = entryService.GetDepartureByEntryId(id);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod()]
        public void GetDepartureByVehicleId_ShouldReturn_DtoDepartureEntity()
        {
            // Arrange
            DepartureEntity departureEntity = new DepartureEntityBuilder()
                                                .WithId(Guid.NewGuid().ToString())
                                                .WithIdVehicle("ASD123")
                                                .Build();

            var vehicleId = departureEntity.IdVehicle;

            _departureRepository.Setup(r => r.List(dr => dr.IdVehicle == vehicleId)).Returns(new List<DepartureEntity> { departureEntity });

            var entryService = new DepartureService(_departureRepository.Object, _entryRepository.Object, _rateService.Object, _cellService.Object);

            // Act
            DepartureEntity result = entryService.GetDepartureByVehicleId(vehicleId);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void GetDepartureByVehicleId_ShouldReturn_Null()
        {
            // Arrange
            DepartureEntity departureEntity = new DepartureEntityBuilder()
                                                .WithId(Guid.NewGuid().ToString())
                                                .WithIdVehicle("ASD123")
                                                .Build();

            var vehicleId = "AAA111";

            _departureRepository.Setup(r => r.List(dr => dr.IdVehicle == vehicleId)).Returns(new List<DepartureEntity>());

            var entryService = new DepartureService(_departureRepository.Object, _entryRepository.Object, _rateService.Object, _cellService.Object);

            // Act
            DepartureEntity result = entryService.GetDepartureByVehicleId(vehicleId);

            // Assert
            Assert.IsNull(result);
        }

    }
}
