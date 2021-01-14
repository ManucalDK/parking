using ParkingAPI.Interfaces;

namespace ParkingAPI.Entities
{
    public class VehicleType: IVehicleType
    {
        public int Id { get; set; }

        public string InternalName { get; set; }

        public string Description { get; set; }
    }
}
