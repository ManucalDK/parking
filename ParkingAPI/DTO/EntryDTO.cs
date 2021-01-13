using Entities.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace ParkingAPI.DTO
{
    public class EntryDTO : IEntry
    {
        [Required]
        public int IdVehicleType { get; set; }

        public string CC { get; set; }

        public DateTime EntryTime { get; set; }

        [Required]
        public string IdVehicle { get; set; }
    }
}
