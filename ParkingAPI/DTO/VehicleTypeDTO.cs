using Entities.Interfaces;

namespace ParkingAPI.DTO
{
    class VehicleTypeDTO: IVehicleType
    {
        public string InternalName { get; set; }

        public string Description { get; set; }
    }
}
