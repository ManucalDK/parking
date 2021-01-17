using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IPlacaService
    {
        bool HasPicoPlaca(int day, int vehicleLastNumberId);

        string GetLastNumberOfIdVehicle(int vehicleTypeId, string vehicleId);
    }
}
