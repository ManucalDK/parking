using Entities.Interfaces;
using System;

namespace Entities.Models
{
    public class Entry: IEntry
    {
        public int Id { get; set; }

        public int IdVehicleType { get; set; }

        public string CC { get; set; }

        public DateTime EntryTime { get; set; }

        public string IdVehicle { get; set; }
    }
}
