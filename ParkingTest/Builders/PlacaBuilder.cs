using AppCore.Entities;
using AppCore.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingTest.Builders
{
    public class PlacaBuilder
    {
        public string Id { get; set; }
        public int Length { get; set; }
        public PlacaType Type { get; set; }
        public int LastNumberFrom { get; set; }

        public PlacaBuilder()
        {
            Id = Guid.NewGuid().ToString();
            Length = 6;
            Type = PlacaType.car;
        }

        public PlacaBuilder WithId(string id)
        {
            Id = id;
            return this;
        }

        public PlacaBuilder WithLength(int length)
        {
            Length = length;
            return this;
        }
        public PlacaBuilder WithType(PlacaType type)
        {
            Type = type;
            return this;
        }

        public PlacaBuilder WithLastNumberFrom(int lastNumberFrom)
        {
            LastNumberFrom = lastNumberFrom;
            return this;
        }

        public PlacaEntity Build()
        {
            return new PlacaEntity()
            {
                Id = Id,
                Length = Length,
                LastNumberFrom = LastNumberFrom,
                Type = Type
            };
        }

    }
}
