using Application.DTOs;
using WebApp.Models;

namespace WebApp.Mappers
{
    public static class DepartureMapper
    {
        public static DepartureModel convertDTOToModel(DtoDeparture dtoDeparture)
        {
            DepartureModel departureModel = new DepartureModel();
            if (dtoDeparture != null)
            {
                departureModel.IdVehicle = dtoDeparture.IdVehicle;
                departureModel.Id = dtoDeparture.Id;
                departureModel.DepartureTime = dtoDeparture.DepartureTime;
                departureModel.RateTotalValue = dtoDeparture.RateValue;
                departureModel.EntryTime = dtoDeparture.EntryTime;
                departureModel.Days = dtoDeparture.Days;
                departureModel.Hours = dtoDeparture.Hours;
            }

            return departureModel;
        }

        public static DtoDeparture convertModelToDTO(DepartureModel departureModel)
        {
            DtoDeparture dtoDeparture = new DtoDeparture();
            if (departureModel != null)
            {
                dtoDeparture.IdVehicle = departureModel.IdVehicle;
            }

            return dtoDeparture;
        }
    }
}
