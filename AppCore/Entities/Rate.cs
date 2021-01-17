using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Entities
{
    public class Rate : EntityBase
    {
        public int IdVehicleType { get; set; }

        public decimal HourValue { get; set; }

        public decimal DayValue { get; set; }
    }
}
