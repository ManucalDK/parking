using System;

namespace AppCore.Entities
{
    public class DepartureEntity : EntityBase
    {
        public DateTime DepartureTime { get; set; }

        public string IdEntry { get; set; }

        public decimal RateTotalValue { get; set; }

        public string IdVehicle { get; set; }
    }
}
