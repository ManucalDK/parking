using System;

namespace Entities.Interfaces
{
    public interface IDeparture
    {
        DateTime DepartureTime { get; set; }

        int IdEntry { get; set; }

        decimal RateTotalValue { get; set; }

        string IdVehicle { get; set; }
    }
}
