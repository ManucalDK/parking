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

        public IEnumerable<DTODeparture> GetDepartures()
        {
            return DepartureMapper.convertEntityToDTO(_departureRepository.List().ToList());
        }

        public DTODeparture GetEntryById(string id)
        {
            return DepartureMapper.convertEntityToDTO(_departureRepository.GetById(id));
        }


        public DepartureEntity GetDepartureByEntryId(string id)
        {
            return _departureRepository.List(dr => dr.IdEntry == id).FirstOrDefault();
        }

        public DepartureEntity GetDepartureByVehicleId(string vehicleId)
        {
            return _departureRepository.List(dr => dr.IdVehicle == vehicleId).FirstOrDefault();
        }

        public DTODeparture RegistryDeparture(DTODeparture departure)
        {
            double totalCharge;
            var departureTime = DateTime.Now;
            EntryEntity lastEntry = _entryRepository.List(er => er.IdVehicle == departure.IdVehicle).LastOrDefault();
            RateEntity rateEntity = _rateService.GetRateByVehicleType(lastEntry.IdVehicleType);
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

            var entryEntity = _departureRepository.Add(DepartureMapper.convertDTOToEntity(departure, lastEntry, totalCharge));
            _cellService.IncreaseCell(lastEntry.IdVehicleType, 1);
            return DepartureMapper.convertEntityToDTO(entryEntity);
        }


    }
}
