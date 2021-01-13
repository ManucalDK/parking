using Entities.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace ParkingAPI.DTO
{
    public class DepartureDTO: IDeparture
    {
        public DateTime DepartureTime { get; set; }

        public int IdEntry { get; set; }

        public decimal RateTotalValue { get; set; }

        [Required]
        public string IdVehicle { get; set; }
    }
}
