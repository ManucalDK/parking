using ParkingAPI.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingAPI.Services.Interfaces
{
    public interface IEntryService
    {
        Task<List<EntryDTO>> GetEntries();

        bool ExistsQuota(int vehicleType);

        Task<int> RegistryVehicle(EntryDTO entry);
    }
}
