using AppCore.Enums;

namespace AppCore.Entities
{
    public class RateEntity : EntityBase
    {
        public VehicleTypeEnum IdVehicleType { get; set; }

        public double HourValue { get; set; }

        public double DayValue { get; set; }

        public double DayChargeFrom { get; set; }

        public double SpecialChargeValue { get; set; }

        public double SpecialChargeFromCC { get; set; }
    }
}
