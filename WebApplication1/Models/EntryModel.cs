using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class EntryModel
    {
        [Required]
        public int IdVehicleType { get; set; }

        public string CC { get; set; }

        [Required]
        public string IdVehicle { get; set; }

        public string Id { get; set; }

        public DateTime entryTime{ get; set; }
    }
}
