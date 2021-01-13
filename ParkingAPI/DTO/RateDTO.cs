using Entities.Interfaces;

namespace ParkingAPI.DTO
{
    class RateDTO: IRate
    {
        public int IdVehicleType { get; set; }

        public decimal HourValue { get; set; }

        public decimal DayValue{ get; set; }
    }
}
