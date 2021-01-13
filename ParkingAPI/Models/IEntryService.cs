using ParkingAPI.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingAPI.Models
{
    public interface IEntryService
    {
        Task<List<EntryDTO>> GetEntries();

        bool ExistsQuota(int vehicleType);

        Task<int> RegistryVehicle(EntryDTO entry);
    }
}
