using AppCore.Enums;

namespace Application.Interfaces
{
    public interface ICellService
    {
        bool ExistsQuotaByVehicleType(VehicleTypeEnum vehicleType);


    }
}
