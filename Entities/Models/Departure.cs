using Entities.Interfaces;
using System;

namespace Entities.Models
{
    public class Departure: IDeparture
    {
        public int Id { get; set; }

        public DateTime DepartureTime { get; set; }

        public int IdEntry { get; set; }

        public decimal RateTotalValue { get; set; }

        public string IdVehicle { get; set; }
    }
}
