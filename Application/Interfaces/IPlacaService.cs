using AppCore.Enums;

namespace Application.Interfaces
{
    public interface IPlacaService
    {
        bool HasPicoPlaca(VehicleTypeEnum vehicleTypeId, int day, int vehicleLastNumberId);

        string GetLastNumberOfIdVehicle(VehicleTypeEnum vehicleTypeId, string vehicleId);
    }
}
