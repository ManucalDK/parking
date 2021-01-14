namespace ParkingAPI.Interfaces
{
    public interface IRate
    {
        int IdVehicleType { get; set; }

        decimal HourValue { get; set; }

        decimal DayValue { get; set; }
    }
}
