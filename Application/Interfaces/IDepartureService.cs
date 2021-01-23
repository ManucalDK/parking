using AppCore.Entities;
using Application.DTOs;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IDepartureService
    {
        DepartureEntity GetDepartureByVehicleId(string vehicleId);

        DepartureEntity GetDepartureByEntryId(string id);

        DtoDeparture RegistryDeparture(DtoDeparture entry);

        IEnumerable<DtoDeparture> GetDepartures();

        DtoDeparture GetEntryById(string id);
    }
}
