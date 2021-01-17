using AppCore.Entities;
using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Mappers
{
    public static class EntryMapper
    {
        public static EntryEntity convertDTOToEntity(DTOEntry entry)
        {
            EntryEntity entryEntity = new EntryEntity();
            if (entry!= null)
            {
                entryEntity.CC = entry.CC;
                entryEntity.EntryTime = DateTime.Now;
                entryEntity.IdVehicle = entry.IdVehicle;
                entryEntity.IdVehicleType = entry.IdVehicleType;
                entryEntity.Id = Guid.NewGuid().ToString();
            }

            return entryEntity;
        }

        public static DTOEntry convertEntityToDTO(EntryEntity entry)
        {
            DTOEntry entryEntity = new DTOEntry();
            if (entry != null)
            {
                entryEntity.CC = entry.CC;
                entryEntity.IdVehicle = entry.IdVehicle;
                entryEntity.IdVehicleType = entry.IdVehicleType;
                entryEntity.Id = entry.Id;
                entryEntity.EntryTime = entry.EntryTime;
            }

            return entryEntity;
        }

        public static IEnumerable<DTOEntry> convertEntityToDTO(List<EntryEntity> entry)
        {
            IEnumerable<DTOEntry> entryEntity = new List<DTOEntry>();
            if (entry?.Count > 0)
            {
                entryEntity = entry.Select(e => new DTOEntry {
                    CC = e.CC,
                    IdVehicle = e.IdVehicle, 
                    IdVehicleType = e.IdVehicleType,
                    Id = e.Id,
                    EntryTime = e.EntryTime
                });
            }

            return entryEntity;
        }
    }
}
