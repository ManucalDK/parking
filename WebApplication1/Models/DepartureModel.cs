using AppCore.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class DepartureModel
    {
        [Required]
        public string IdVehicle { get; set; }

        public string Id { get; set; }

        public DateTime DepartureTime{ get; set; }

        public double RateTotalValue{ get; set; }

        public DateTime EntryTime { get; set; }

        public int Days { get; set; }

        public double Hours { get; set; }
    }
}
