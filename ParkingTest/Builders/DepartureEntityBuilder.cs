using AppCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTest.Builders
{
    public class DepartureEntityBuilder
    {
        public DateTime DepartureTime { get; set; }
        public string IdEntry { get; set; }
        public decimal RateTotalValue { get; set; }
        public string IdVehicle { get; set; }
        public string Id { get; set; }

        public DepartureEntityBuilder()
        {
            DepartureTime = DateTime.Now;
            IdEntry = Guid.NewGuid().ToString();
            RateTotalValue = 5000m;
            IdVehicle = "SFL55D";
        }

        public DepartureEntityBuilder WithDepartureTime(DateTime dateTime)
        {
            DepartureTime = dateTime;
            return this;
        }

        public DepartureEntityBuilder WithIdEntry(string idEntry)
        {
            IdEntry = idEntry;
            return this;
        }

        public DepartureEntityBuilder WithRateTotalValue(decimal rate)
        {
            RateTotalValue = rate;
            return this;
        }

        public DepartureEntityBuilder WithIdVehicle(string idVehicle)
        {
            IdVehicle = idVehicle;
            return this;
        }

        public DepartureEntityBuilder WithId(string id)
        {
            Id = id;
            return this;
        }

        public DepartureEntity Build()
        {
            return new DepartureEntity()
            {
                DepartureTime = DepartureTime,
                Id = Id,
                IdEntry = IdEntry,
                IdVehicle = IdVehicle,
                RateTotalValue = RateTotalValue
            };
        }


    }
}
