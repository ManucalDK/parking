using Application.DTOs;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IEntryService
    {
        IEnumerable<DTOEntry> GetEntries();

        DTOEntry RegistryVehicle(DTOEntry entry);

        int GetEntriesByVehicleType(int vehicleType);

        DTOEntry GetEntryById(string id);
    }
}
