using AppCore.Entities;
using AppCore.Enums;

namespace Application.Interfaces
{
    public interface ICellService
    {
        bool ExistsQuotaByVehicleType(VehicleTypeEnum vehicleType);

        CellEntity DecreaseCell(VehicleTypeEnum vehicleType, int decrease);

        CellEntity IncreaseCell(VehicleTypeEnum vehicleType, int increase);

    }
}
