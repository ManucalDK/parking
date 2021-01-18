using AppCore.Entities;
using Application.DTOs;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IEntryService
    {
        IEnumerable<DTOEntry> GetEntries();

        DTOEntry RegistryVehicle(DTOEntry entry);

        DTOEntry GetEntryById(string id);

        EntryEntity GetLastEntryByVehicleId(string id);
    }
}
