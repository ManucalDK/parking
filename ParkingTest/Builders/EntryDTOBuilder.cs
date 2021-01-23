using AppCore.Enums;
using Application.DTOs;

namespace ParkingTest.Builders
{
    public class EntryDTOBuilder
    {
        public VehicleTypeEnum IdVehicleType { get; set; }
        public string CC { get; set; }
        public string IdVehicle { get; set; }

        public EntryDTOBuilder()
        {
            IdVehicleType = VehicleTypeEnum.motorcycle;
            IdVehicle = "SFL55D";
        }

        public EntryDTOBuilder WithVehicleId(string vehicleId)
        {
            IdVehicle = vehicleId;
            return this;
        }

        public EntryDTOBuilder WithVehicleType(VehicleTypeEnum vehicleId)
        {
            IdVehicleType = vehicleId;
            return this;
        }

        public EntryDTOBuilder WithCC(string cc )
        {
            CC = cc;
            return this;
        }

        public DtoEntry Build()
        {
            return new DtoEntry() { 
                CC = CC,
                IdVehicle = IdVehicle,
                IdVehicleType = IdVehicleType
            };
        }
    }
}
