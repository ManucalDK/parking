using AppCore.Entities;
using AppCore.Enums;

namespace Application.Interfaces
{
    public interface IRateService
    {
        RateEntity GetRateByVehicleType(VehicleTypeEnum vehicleType);
    }
}
