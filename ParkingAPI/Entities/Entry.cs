using ParkingAPI.Interfaces;
using System;

namespace ParkingAPI.Entities
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
