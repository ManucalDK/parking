using Application.DTOs;
using WebApp.Models;

namespace WebApp.Mappers
{
    public static class EntryMapper
    {
        public static EntryModel convertDTOToModel(DTOEntry entry)
        {
            EntryModel entryModel = new EntryModel();
            if (entry != null)
            {
                entryModel.CC = entry.CC;
                entryModel.IdVehicle = entry.IdVehicle;
                entryModel.IdVehicleType = entry.IdVehicleType;
                entryModel.Id = entry.Id;
                entryModel.EntryTime = entry.EntryTime;
            }

            return entryModel;
        }

        public static DTOEntry convertModelToDTO(EntryModel entry)
        {
            DTOEntry dtoEntry = new DTOEntry();
            if (entry != null)
            {
                dtoEntry.CC = entry.CC;
                dtoEntry.IdVehicle = entry.IdVehicle;
                dtoEntry.IdVehicleType = entry.IdVehicleType;
            }

            return dtoEntry;
        }
    }
}
