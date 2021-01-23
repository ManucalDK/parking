using AppCore.Entities;
using Application.DTOs;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IEntryService
    {
        IEnumerable<DtoEntry> GetEntries();

        DtoEntry RegistryVehicle(DtoEntry entry);

        DtoEntry GetEntryById(string id);

        EntryEntity GetLastEntryByVehicleId(string id);
    }
}
