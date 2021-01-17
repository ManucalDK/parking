using AppCore.Entities;
using Application.Interfaces;
using System;

namespace Application.Services
{
    public class DepartureService : IDepartureService
    {
        IRepository<DepartureEntity> _repository;

        public DepartureService(IRepository<DepartureEntity> repository)
        {
            _repository = repository;
        }

        public DepartureEntity GetDepartureByEntryId(string id)
        {
            throw new NotImplementedException();
        }

        public DepartureEntity GetDepartureByVehicleId(string vehicleId)
        {
            throw new NotImplementedException();
        }
    }
}
