using AppCore.Enums;
using System.Collections.Generic;

namespace AppCore.Entities
{
    public class PlacaEntity : EntityBase
    {
        public int Length { get; set; }

        public VehicleTypeEnum Type { get; set; }

        public int LastNumberFrom { get; set; }

        public List<PicoPlacaDigits> PicoPlacaDigits { get; set; }
    }
}
