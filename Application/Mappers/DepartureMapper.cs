using AppCore.Entities;
using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Mappers
{
    public static class DepartureMapper
    {
        public static DepartureEntity convertDTOToEntity(DTODeparture dtoDeparture, EntryEntity entryEntity, double rateTotal)
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

        public static DTODeparture convertEntityToDTO(DepartureEntity departureEntity)
        {
            DTODeparture dtoDeparture = new DTODeparture();
            if (departureEntity != null)
            {
                dtoDeparture.Id = departureEntity.Id;
                dtoDeparture.DepartureTime = departureEntity.DepartureTime;
                dtoDeparture.RateValue = departureEntity.RateTotalValue;
                dtoDeparture.IdVehicle = departureEntity.IdVehicle;
            }

            return dtoDeparture;
        }

        public static IEnumerable<DTODeparture> convertEntityToDTO(List<DepartureEntity> departureEntity)
        {
            IEnumerable<DTODeparture> dtoDeparture = new List<DTODeparture>();
            if (departureEntity?.Count > 0)
            {
                dtoDeparture = departureEntity.Select(e => new DTODeparture()
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
