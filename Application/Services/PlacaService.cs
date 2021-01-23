using AppCore.Enums;
using Application.Interfaces;
using System.Linq;
using AppCore.Exceptions;
using AppCore.Entities;

namespace Application.Services
{
    public class PlacaService : IPlacaService
    {
        IRepository<PlacaEntity> _repository;
        IRepository<PicoPlacaDigits> _picoPlacarepository;
        


        public PlacaService(IRepository<PlacaEntity> repository, IRepository<PicoPlacaDigits> picoPlacarepository)
        {
            _repository = repository;
            _picoPlacarepository = picoPlacarepository;
        }

        public bool HasPicoPlaca(VehicleTypeEnum vehicleTypeId, int day, int vehicleLastNumberId)
        {
            var result = _repository.List(repo => repo.Type == vehicleTypeId).FirstOrDefault();
            var picoPlacaResult = _picoPlacarepository.List(repo => repo.PlacaEntityID == result.Id && repo.Day == day && repo.Digit == vehicleLastNumberId);
            return picoPlacaResult?.Count() > 0;
        }

        public string GetLastNumberOfIdVehicle(VehicleTypeEnum vehicleTypeId, string vehicleId)
        {
            string lastNumber = string.Empty;
            if (vehicleTypeId == VehicleTypeEnum.car)
            {
                lastNumber = vehicleId.Last().ToString();
            }

            if (vehicleTypeId == VehicleTypeEnum.motorcycle)
            {
                var placa = _repository.List(r => r.Type == VehicleTypeEnum.motorcycle).FirstOrDefault();
                if (vehicleId.Length < placa.Length)
                {
                    throw new PlacaException($"La placa del vehículo posee un problema en el formato. La longitud debe ser de {placa.Length}");
                }
                lastNumber = vehicleId.TakeLast(placa.LastNumberFrom).FirstOrDefault().ToString();
            }

            return lastNumber;

        }
    }
}
