using AppCore.Entities;
using AppCore.Enums;
using Application.Interfaces;
using Application.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace ParkingTest.Unit.Services
{
    [TestClass()]
    public class RateServiceTests
    {
        Mock<IRepository<RateEntity>> _rateRepository;

        [TestInitialize]
        public void Setup()
        {
            // repos
            _rateRepository = new Mock<IRepository<RateEntity>>();
        }

        [TestMethod()]
        public void GetRateByVehicleType_WithConfig_ShouldReturnRateEntity()
        {
            var vehicleType = VehicleTypeEnum.car;
            var rateEntityList = new List<RateEntity> { new RateEntity {
                Id = Guid.NewGuid().ToString()
            } };

            _rateRepository.Setup(rm => rm.List(rr => rr.IdVehicleType == vehicleType)).Returns(rateEntityList);

            var rateService = new RateService(_rateRepository.Object);

            var rateEntity = rateService.GetRateByVehicleType(vehicleType);

            Assert.IsTrue(typeof(RateEntity) == rateEntity.GetType());
        }


        [TestMethod()]
        public void GetRateByVehicleType_WithoutConfig_ShouldReturnNull()
        {
            var vehicleType = VehicleTypeEnum.car;
            var rateEntityList = new List<RateEntity>();

            _rateRepository.Setup(rm => rm.List(rr => rr.IdVehicleType == vehicleType)).Returns(rateEntityList);

            var rateService = new RateService(_rateRepository.Object);

            var rateEntity = rateService.GetRateByVehicleType(VehicleTypeEnum.motorcycle);

            Assert.IsNull(rateEntity);
        }
    }
}
