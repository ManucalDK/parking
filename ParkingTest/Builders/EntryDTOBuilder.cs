using AppCore.Enums;
using Application.DTOs;

namespace ParkingTest.Builders
{
    public class EntryDTOBuilder
    {
        public int IdVehicleType { get; set; }
        public string CC { get; set; }
        public string IdVehicle { get; set; }

        public EntryDTOBuilder()
        {
            IdVehicleType = (int)VehicleTypeEnum.motorcycle;
            IdVehicle = "SFL55D";
        }

        public EntryDTOBuilder WithVehicleId(string vehicleId)
        {
            IdVehicle = vehicleId;
            return this;
        }

        public EntryDTOBuilder WithVehicleType(VehicleTypeEnum vehicleId)
        {
            IdVehicleType = (int)vehicleId;
            return this;
        }

        public EntryDTOBuilder WithCC(string cc )
        {
            CC = cc;
            return this;
        }

        public DTOEntry Build()
        {
            return new DTOEntry() { 
                CC = CC,
                IdVehicle = IdVehicle,
                IdVehicleType = IdVehicleType
            };
        }
    }
}
