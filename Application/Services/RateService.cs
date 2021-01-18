using AppCore.Entities;
using AppCore.Enums;
using Application.Interfaces;
using System.Linq;

namespace Application.Services
{
    public class RateService : IRateService
    {
        IRepository<RateEntity> _rateRepository;
        public RateService(IRepository<RateEntity> rateRepository)
        {
            _rateRepository = rateRepository;
        }
        public RateEntity GetRateByVehicleType(VehicleTypeEnum vehicleType)
        {
            return _rateRepository.List(rr => rr.IdVehicleType == vehicleType).FirstOrDefault();
        }
    }
}
