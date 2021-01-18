using Application.DTOs;
using WebApp.Models;

namespace WebApp.Mappers
{
    public static class DepartureMapper
    {
        public static DepartureModel convertDTOToModel(DTODeparture dtoDeparture)
        {
            DepartureModel departureModel = new DepartureModel();
            if (dtoDeparture != null)
            {
                departureModel.IdVehicle = dtoDeparture.IdVehicle;
                departureModel.Id = dtoDeparture.Id;
                departureModel.DepartureTime = dtoDeparture.DepartureTime;
                departureModel.RateTotalValue = dtoDeparture.RateValue;
            }

            return departureModel;
        }

        public static DTODeparture convertModelToDTO(DepartureModel departureModel)
        {
            DTODeparture dtoDeparture = new DTODeparture();
            if (departureModel != null)
            {
                dtoDeparture.IdVehicle = departureModel.IdVehicle;
            }

            return dtoDeparture;
        }
    }
}
