using System;

namespace AppCore.Entities
{
    public class EntryEntity : EntityBase
    {
        public int IdVehicleType { get; set; }

        public string CC { get; set; }

        public DateTime EntryTime { get; set; }

        public string IdVehicle { get; set; }
    }
}
