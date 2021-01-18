﻿using AppCore.Enums;
using System;

namespace Application.DTOs
{
    public class DTOEntry : BaseDTO
    {
        public VehicleTypeEnum IdVehicleType { get; set; }

        public string CC { get; set; }

        public string IdVehicle { get; set; }

        public DateTime EntryTime { get; set; }
    }
}
