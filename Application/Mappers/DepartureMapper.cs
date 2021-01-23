using AppCore.Entities;
using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Mappers
{
    public static class DepartureMapper
    {
        public static DepartureEntity ConvertDTOToEntity(DtoDeparture dtoDeparture, EntryEntity entryEntity, double rateTotal)
        {
            DepartureEntity departureEntity = new DepartureEntity();
            if (dtoDeparture != null)
            {
                departureEntity.DepartureTime = DateTime.Now;
                departureEntity.IdVehicle = dtoDeparture.IdVehicle;
                departureEntity.Id = Guid.NewGuid().ToString();
                departureEntity.IdEntry = entryEntity.Id;
                departureEntity.RateTotalValue = rateTotal;
            }

            return departureEntity;
        }

        public static DtoDeparture ConvertEntityToDTO(DepartureEntity departureEntity)
        {
            DtoDeparture dtoDeparture = new DtoDeparture();
            if (departureEntity != null)
            {
                dtoDeparture.Id = departureEntity.Id;
                dtoDeparture.DepartureTime = departureEntity.DepartureTime;
                dtoDeparture.RateValue = departureEntity.RateTotalValue;
                dtoDeparture.IdVehicle = departureEntity.IdVehicle;
            }

            return dtoDeparture;
        }

        public static IEnumerable<DtoDeparture> ConvertEntityToDTO(List<DepartureEntity> departureEntity)
        {
            IEnumerable<DtoDeparture> dtoDeparture = new List<DtoDeparture>();
            if (departureEntity?.Count > 0)
            {
                dtoDeparture = departureEntity.Select(e => new DtoDeparture()
                {
                    Id = e.Id,
                    DepartureTime = e.DepartureTime,
                    RateValue = e.RateTotalValue,
                    IdVehicle = e.IdVehicle
                });
                
            }

            return dtoDeparture;
        }
    }
}
