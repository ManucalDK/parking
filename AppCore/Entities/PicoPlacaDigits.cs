using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Entities
{
    public class PicoPlacaDigits : EntityBase
    {
        public int Digit { get; set; }

        public int Day { get; set; }

        public string PlacaEntityID { get; set; }

        public PlacaEntity PlacaEntity { get; set; }
    }
}
