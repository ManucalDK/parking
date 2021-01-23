using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTest.Builders
{
    public class DepartureDTOBuilder
    {
        public string Id { get; set; }
        public string IdVehicle { get; set; }
        public DateTime DepartureTime { get; set; }
        public double RateValue { get; set; }

        public DepartureDTOBuilder()
        {
            Id = Guid.NewGuid().ToString();
            IdVehicle = "AAA111";
            DepartureTime = DateTime.Now;
            RateValue = 2000d;
        }

        public DepartureDTOBuilder WithIdVehicle(string vehicleId)
        {
            IdVehicle = vehicleId;
            return this;
        }

        public DepartureDTOBuilder WithId(string id)
        {
            Id = id;
            return this;
        }

        public DepartureDTOBuilder WithDepartureTime(DateTime departureTime)
        {
            DepartureTime = departureTime;
            return this;
        }

        public DepartureDTOBuilder WithRateValue(double value)
        {
            RateValue = value;
            return this;
        }

        public DtoDeparture Build()
        {
            return new DtoDeparture()
            {
                Id = Id,
                IdVehicle = IdVehicle,
                DepartureTime = DepartureTime,
                RateValue = RateValue
            };
        }
    }
}
