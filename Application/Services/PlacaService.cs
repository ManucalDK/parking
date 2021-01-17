using AppCore.Enums;
using Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using AppCore.Exceptions;
using AppCore.Entities;

namespace Application.Services
{
    public class PlacaService : IPlacaService
    {
        IRepository<PlacaEntity> _repository;

        public PlacaService(IRepository<PlacaEntity> repository)
        {
            _repository = repository;
        }

        public bool HasPicoPlaca(int day, int vehicleLastNumberId)
        {
            var picoPlacaDays = new Dictionary<int, int[]>
            {
                { 1, new int[] { 9, 0, 1 } },
                { 2, new int[] { 2, 3 } },
                { 3, new int[] { 4, 5 } },
                { 4, new int[] { 5, 6, 7 } },
                { 5, new int[] { 8, 0 } }
            };

            var findResult = picoPlacaDays.Where(d => d.Key == day &&
                                                        d.Value.Where(p => p == vehicleLastNumberId).Select(p => p).FirstOrDefault() != 0)
                                          .Select(p => p.Key)
                                          .FirstOrDefault();

            return findResult != 0;
        }
        public string GetLastNumberOfIdVehicle(int vehicleTypeId, string vehicleId)
        {
            string lastNumber = string.Empty;
            if (vehicleTypeId == (int)VehicleTypeEnum.car)
            {
                lastNumber = vehicleId.Last().ToString();
            }

            if (vehicleTypeId == (int)VehicleTypeEnum.motorcycle)
            {
                var placa = _repository.List(r => r.Type == PlacaType.motorcycle).FirstOrDefault();
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
