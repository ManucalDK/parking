using AppCore.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class EntryModel
    {
        [Required]
        public VehicleTypeEnum IdVehicleType { get; set; }

        public string CC { get; set; }

        [Required]
        [MaxLength(6, ErrorMessage ="La placa no debe sobrepasar los 6 carácteres")]
        [MinLength(6, ErrorMessage ="La placa debe ser de mínimo 6 carácteres")]
        public string IdVehicle { get; set; }

        public string Id { get; set; }

        public DateTime EntryTime{ get; set; }
    }
}
