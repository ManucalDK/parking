using AppCore.Entities;
using Application.DTOs;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using AppCore.Exceptions;
using AppCore.Enums;
using Application.Mappers;

namespace Application.Services
{
    public class EntryService : IEntryService
    {
        IRepository<EntryEntity> _entryRepository;
        ICellService _cellService;
        IDepartureService _departureService;
        IPlacaService _placaService;

        public EntryService(IRepository<EntryEntity> entryService, ICellService cellService, IDepartureService departureService, IPlacaService placaService)
        {
            _entryRepository = entryService;
            _cellService = cellService;
            _departureService = departureService;
            _placaService = placaService;
        }

        public DTOEntry GetEntryById(string id)
        {
            return EntryMapper.convertEntityToDTO(_entryRepository.GetById(id));
        }

        public IEnumerable<DTOEntry> GetEntries()
        {
            return EntryMapper.convertEntityToDTO(_entryRepository.List().ToList());
        }

        public int GetEntriesByVehicleType(int vehicleType)
        {
            EntryEntity entry = _entryRepository.List(e => e.IdVehicle == "SFL55D").FirstOrDefault();
            return 1;
        }

        public DTOEntry RegistryVehicle(DTOEntry entry)
        {
            var lastEntryByIdVehicle = _entryRepository.List(e => e.IdVehicle == entry.IdVehicle).LastOrDefault();

            if (lastEntryByIdVehicle != null)
            {
                var departure = _departureService.GetDepartureByEntryId(lastEntryByIdVehicle.Id);
                if (departure == null)
                {
                    throw new EntryException("El vehículo que está registrando posee una salida pendiente");
                }
            }

            if (!_cellService.ExistsQuotaByVehicleType((VehicleTypeEnum)entry.IdVehicleType))
            {
                throw new CellException("No hay cupos disponibles");
            }

            string lastNumberIdVehicle = _placaService.GetLastNumberOfIdVehicle(entry.IdVehicleType, entry.IdVehicle);
            bool isParsed = short.TryParse(lastNumberIdVehicle, out short numberResult);

            if (!isParsed)
            {
                throw new EntryException("Hubo un problema al leer la placa del vehículo. Verifique el tipo de vehículo e intente de nuevo");
            }

            if (_placaService.HasPicoPlaca((int)DateTime.Now.DayOfWeek, numberResult))
            {
                throw new EntryException("El vehículo no puede ser registrado, tiene pico y placa.");
            }

            if ((entry.IdVehicleType == (int)VehicleTypeEnum.motorcycle) && string.IsNullOrEmpty(entry.CC))
            {
                throw new EntryException("Falta la información del cilindraje de la motocicleta");
            }

            var entryEntity = _entryRepository.Add(EntryMapper.convertDTOToEntity(entry));

            return EntryMapper.convertEntityToDTO(entryEntity);
        }
    
    
    }
}
