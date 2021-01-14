using ParkingAPI.Interfaces;

namespace ParkingAPI.Entities
{
    public class Rate: IRate
    {
        public int Id { get; set; }

        public int IdVehicleType { get; set; }

        public decimal HourValue { get; set; }

        public decimal DayValue { get; set; }

    }
}
