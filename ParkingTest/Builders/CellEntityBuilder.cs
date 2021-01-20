using AppCore.Entities;
using AppCore.Enums;
using System;

namespace ParkingTest.Builders
{
    public class CellEntityBuilder
    {
        public string Id { get; set; }

        public VehicleTypeEnum IdVehicleType { get; set; }

        public int NumCellAvaliable { get; set; }

        public int NumTotalCells { get; set; }

        public CellEntityBuilder()
        {
            Id = Guid.NewGuid().ToString();
            IdVehicleType = VehicleTypeEnum.car;
            NumCellAvaliable = 20;
            NumTotalCells = 20;
        }

        public CellEntityBuilder WithId(string id)
        {
            Id = id;
            return this;
        }

        public CellEntityBuilder WithVehicleType(VehicleTypeEnum vehicleTypeEnum)
        {
            IdVehicleType = vehicleTypeEnum;
            return this;
        }

        public CellEntityBuilder WithNumCellAvaliable(int numCellAvaliable)
        {
            NumCellAvaliable = numCellAvaliable;
            return this;
        }

        public CellEntityBuilder WithNumTotalCells(int numTotalCell)
        {
            NumTotalCells = numTotalCell;
            return this;
        }

        public CellEntity Build()
        {
            return new CellEntity()
            {
                Id = Id,
                NumTotalCells = NumTotalCells,
                IdVehicleType = IdVehicleType,
                NumCellAvaliable = NumCellAvaliable
            };
        }
    }
}
