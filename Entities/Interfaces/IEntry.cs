using System;

namespace Entities.Interfaces
{
    public interface IEntry
    {
        int IdVehicleType { get; set; }

        string CC { get; set; }

        string IdVehicle { get; set; }
    }
}
