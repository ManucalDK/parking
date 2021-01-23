using AppCore.Entities;
using AppCore.Enums;
using AppCore.Exceptions;
using Application.DTOs;
using Application.Interfaces;
using Application.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class DepartureService : IDepartureService
    {
        IRepository<DepartureEntity> _departureRepository;
        IRepository<EntryEntity> _entryRepository;
        IRateService _rateService;
        ICellService _cellService;

        public DepartureService(IRepository<DepartureEntity> departureRepository, IRepository<EntryEntity> entryRepository,  IRateService rateService, ICellService cellService)
        {
            _departureRepository = departureRepository;
            _entryRepository = entryRepository;
            _rateService = rateService;
            _cellService = cellService;
        }

        public IEnumerable<DtoDeparture> GetDepartures()
        {
            return DepartureMapper.ConvertEntityToDTO(_departureRepository.List().ToList());
        }

        public DtoDeparture GetEntryById(string id)
        {
            return DepartureMapper.ConvertEntityToDTO(_departureRepository.GetById(id));
        }


        public DepartureEntity GetDepartureByEntryId(string id)
        {
            return _departureRepository.List(dr => dr.IdEntry == id).FirstOrDefault();
        }

        public DepartureEntity GetDepartureByVehicleId(string vehicleId)
        {
            return _departureRepository.List(dr => dr.IdVehicle == vehicleId).FirstOrDefault();
        }

        public DtoDeparture RegistryDeparture(DtoDeparture departure)
        {
            double totalCharge;
            var departureTime = DateTime.Now;
            EntryEntity lastEntry = GetInfoEntryByVehicleId(departure.IdVehicle);

            if (lastEntry == null)
            {
                throw new DepartureException("No existe un registro de entrada para el vehículo");
            }

            RateEntity rateEntity = _rateService.GetRateByVehicleType(lastEntry.IdVehicleType);

            if(rateEntity == null)
            {
                throw new DepartureException("No existe una tarifa configurada para el tipo de vehículo");
            }

            var difference = departureTime - lastEntry.EntryTime;//Math.Ceiling((departureTime - lastEntry.EntryTime).TotalHours);
            int days = difference.Days;
            double hours = Math.Ceiling(difference.TotalHours);

            if(days < 1)
            {
                if (hours >= rateEntity.DayChargeFrom)
                {
                    totalCharge = rateEntity.DayValue;
                } else
                {
                    totalCharge = rateEntity.HourValue * hours;
                }
            } else
            {
                var additionalHours = hours % 24;
                totalCharge = days * rateEntity.DayValue;
                totalCharge += additionalHours * rateEntity.HourValue;
            }

            if(lastEntry.IdVehicleType == VehicleTypeEnum.motorcycle)
            {
                bool isParsed = short.TryParse(lastEntry.CC, out short cc);
                if (!isParsed)
                {
                    throw new DepartureException("No fue posible determinar el cilindraje del vehículo");
                }
                if (cc >= rateEntity.SpecialChargeFromCC)
                {
                    totalCharge += rateEntity.SpecialChargeValue;
                }
            }

            var entryEntity = _departureRepository.Add(DepartureMapper.ConvertDTOToEntity(departure, lastEntry, totalCharge));
            _cellService.IncreaseCell(lastEntry.IdVehicleType, 1);
            return DepartureMapper.ConvertEntityToDTO(entryEntity);
        }

        private EntryEntity GetInfoEntryByVehicleId(string idVehicle)
        {
            return  _entryRepository.List(er => er.IdVehicle == idVehicle).LastOrDefault();
        }


    }
}
