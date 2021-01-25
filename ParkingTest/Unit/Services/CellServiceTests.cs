using AppCore.Entities;
using AppCore.Enums;
using AppCore.Exceptions;
using Application.Interfaces;
using Application.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace ParkingTest.Unit.Services
{
    [TestClass]
    public class CellServiceTests
    {
        Mock<IRepository<CellEntity>> _cellRepository;

        [TestInitialize]
        public void Setup()
        {
            _cellRepository = new Mock<IRepository<CellEntity>>();
        }

        [TestMethod, ExpectedException(typeof(CellException))]
        public void IncreaseCell_WithSame_CellAvaliable_And_NumTotalCells_ShouldReturnAnException()
        {
            var vehicleType = VehicleTypeEnum.car;
            var listCells = new List<CellEntity> {
                new CellEntity{
                    Id = Guid.NewGuid().ToString(),
                    IdVehicleType = vehicleType,
                    NumCellAvaliable = 4,
                    NumTotalCells = 4
                } };
            var cellExceptionMessage = "No hay más celdas disponibles";

            _cellRepository.Setup(cs => cs.List(cell => cell.IdVehicleType == vehicleType)).Returns(listCells);

            var cellService = new CellService(_cellRepository.Object);

            try
            {
                cellService.IncreaseCell(vehicleType, 1);
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, cellExceptionMessage);
                throw;
            }
        }

        [TestMethod]
        public void IncreaseCell_Should_Return_NumCellAvaliable_To4()
        {
            var vehicleType = VehicleTypeEnum.car;
            var listCells = new List<CellEntity> {
                new CellEntity{
                    Id = Guid.NewGuid().ToString(),
                    IdVehicleType = vehicleType,
                    NumCellAvaliable = 3,
                    NumTotalCells = 4
                } };

            _cellRepository.Setup(cs => cs.List(cell => cell.IdVehicleType == vehicleType)).Returns(listCells);

            var cellService = new CellService(_cellRepository.Object);


            var response = cellService.IncreaseCell(vehicleType, 1);

            Assert.IsTrue(response.NumCellAvaliable == 4);

        }

        [TestMethod, ExpectedException(typeof(CellException))]
        public void DecreaseCell_WithoutAvaliableCells_ShouldReturnAnException()
        {
            var vehicleType = VehicleTypeEnum.car;
            var listCells = new List<CellEntity> {
                new CellEntity{
                    Id = Guid.NewGuid().ToString(),
                    IdVehicleType = vehicleType,
                    NumCellAvaliable = 0,
                    NumTotalCells = 4
                } };
            var cellExceptionMessage = "No hay más celdas disponibles";

            _cellRepository.Setup(cs => cs.List(cell => cell.IdVehicleType == vehicleType)).Returns(listCells);

            var cellService = new CellService(_cellRepository.Object);

            try
            {
                cellService.DecreaseCell(vehicleType, 1);
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, cellExceptionMessage);
                throw;
            }
        }

        [TestMethod]
        public void DecreaseCell_Should_Return_NumCellAvaliable_To3()
        {
            var vehicleType = VehicleTypeEnum.car;
            var listCells = new List<CellEntity> {
                new CellEntity{
                    Id = Guid.NewGuid().ToString(),
                    IdVehicleType = vehicleType,
                    NumCellAvaliable = 4,
                    NumTotalCells = 4
                } };

            _cellRepository.Setup(cs => cs.List(cell => cell.IdVehicleType == vehicleType)).Returns(listCells);

            var cellService = new CellService(_cellRepository.Object);


            var response = cellService.DecreaseCell(vehicleType, 1);

            Assert.IsTrue(response.NumCellAvaliable == 3);

        }
    }
}
