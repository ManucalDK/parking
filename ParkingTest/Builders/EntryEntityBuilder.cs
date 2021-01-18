using AppCore.Entities;
using AppCore.Enums;
using Application.DTOs;
using System;

namespace ParkingTest.Builders
{
    public class EntryEntityBuilder
    {
        public VehicleTypeEnum IdVehicleType { get; set; }
        public string CC { get; set; }
        public string IdVehicle { get; set; }
        public DateTime EntryTime { get; set; }
        public string Id { get; set; }


        public EntryEntityBuilder()
        {
            IdVehicleType = VehicleTypeEnum.motorcycle;
            IdVehicle = "SFL55D";
            CC = "100";
            EntryTime = DateTime.Now;
            Id = Guid.NewGuid().ToString();
        }

        public EntryEntityBuilder WithVehicleId(string vehicleId)
        {
            IdVehicle = vehicleId;
            return this;
        }

        public EntryEntityBuilder WithVehicleType(VehicleTypeEnum vehicleId)
        {
            IdVehicleType = vehicleId;
            return this;
        }

        public EntryEntityBuilder WithCC(string cc )
        {
            CC = cc;
            return this;
        }

        public EntryEntityBuilder WithEntryTime(DateTime dateTime)
        {
            EntryTime = dateTime;
            return this;
        }

        public EntryEntityBuilder WithId(string id)
        {
            Id = id;
            return this;
        }

        public EntryEntity Build()
        {
            return new EntryEntity() { 
                CC = CC,
                IdVehicle = IdVehicle,
                IdVehicleType = IdVehicleType,
                Id = Id,
                EntryTime = EntryTime
            };
        }
    }
}
