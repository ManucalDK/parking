using AppCore.Entities;
using Application.DTOs;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IDepartureService
    {
        DepartureEntity GetDepartureByVehicleId(string vehicleId);

        DepartureEntity GetDepartureByEntryId(string id);

        DTODeparture RegistryDeparture(DTODeparture entry);

        IEnumerable<DTODeparture> GetDepartures();

        DTODeparture GetEntryById(string id);
    }
}
