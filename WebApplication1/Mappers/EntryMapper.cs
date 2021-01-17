using Application.DTOs;
using System;
using System.Collections.Generic;
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
                entryModel.entryTime = entry.EntryTime;
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

        //public static IEnumerable<DTOEntry> convertEntityToDTO(List<DTOEntry> entry)
        //{
        //    IEnumerable<DTOEntry> entryEntity = new List<DTOEntry>();
        //    if (entry?.Count > 0)
        //    {
        //        entryEntity = entry.Select(e => new DTOEntry
        //        {
        //            CC = e.CC,
        //            IdVehicle = e.IdVehicle,
        //            IdVehicleType = e.IdVehicleType
        //        });
        //    }

        //    return entryEntity;
        //}
    }
}
