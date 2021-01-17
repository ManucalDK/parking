using AppCore.Entities;

namespace Application.Interfaces
{
    public interface IDepartureService
    {
        DepartureEntity GetDepartureByVehicleId(string vehicleId);

        DepartureEntity GetDepartureByEntryId(string id);
    }
}
