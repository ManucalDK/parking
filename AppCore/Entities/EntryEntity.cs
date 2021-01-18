using AppCore.Enums;
using System;

namespace AppCore.Entities
{
    public class EntryEntity : EntityBase
    {
        public VehicleTypeEnum IdVehicleType { get; set; }

        public string CC { get; set; }

        public DateTime EntryTime { get; set; }

        public string IdVehicle { get; set; }
    }
}
