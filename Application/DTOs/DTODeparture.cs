using AppCore.Enums;
using System;

namespace Application.DTOs
{
    public class DTODeparture : BaseDTO
    {

        public string IdVehicle { get; set; }

        public DateTime EntryTime { get; set; }

        public DateTime DepartureTime { get; set; }

        public double RateValue { get; set; }

        public int Days { get; set; }

        public double Hours { get; set; }
    }
}
