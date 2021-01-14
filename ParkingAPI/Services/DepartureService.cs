using ParkingAPI.DTO;
using ParkingAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using ParkingAPI.Entities;
using System;
using ParkingAPI.Exceptions;
using System.Collections.Generic;
using ParkingAPI.Enums;
using ParkingAPI.Services.Interfaces;

namespace ParkingAPI.Services
{
    public class DepartureService: IDepartureService
    {
        private readonly ParkingContext _context;
        const int day = 9;
        const int CCLimitSurcharge = 500;
        const int dayhours = 24;

        public DepartureService(ParkingContext context)
        {
            _context = context;
        }

        public async Task<List<DepartureDTO>> GetDepartures()
        {
            var entriesResult = await _context.Departures.Select(d =>
                                      new DepartureDTO()
                                      {
                                          DepartureTime = d.DepartureTime,
                                          IdEntry = d.IdEntry,
                                          IdVehicle = d.IdVehicle,
                                          RateTotalValue = d.RateTotalValue
                                      }).ToListAsync();
            return entriesResult;
        }

        public async Task<DepartureDTO> GetCellByIdEntry(int id) => await _context.Departures.Where(d => d.IdEntry == id).Select(d =>
                                                                  new DepartureDTO()
                                                                  {
                                                                      DepartureTime = d.DepartureTime,
                                                                      IdEntry = d.IdEntry,
                                                                      RateTotalValue = d.RateTotalValue
                                                                  }).FirstOrDefaultAsync();
        public async Task<int> SetDeparture(DepartureDTO departure)
        {
            var departureTime = DateTime.Now;
            var entry = await _context.Entries
                                .Where(e => e.IdVehicle == departure.IdVehicle)
                                .LastOrDefaultAsync();
            var departureExists = await _context.Departures
                                    .Where(d => d.IdVehicle == departure.IdVehicle)
                                    .LastOrDefaultAsync();

            if(entry == null)
            {
                throw new DepartureException("The vehicle doesn't registry an entry");
            }

            if(departureExists?.IdEntry == entry.Id)
            {
                throw new DepartureException("The vehicle already register a departure");
            } 
          

            Departure entryModel = new Departure();
            var lastDeparture = await _context.Departures.LastOrDefaultAsync();
            entryModel.Id = lastDeparture != null ? lastDeparture.Id + 1 : 1;
            entryModel.DepartureTime = departureTime;
            entryModel.IdEntry = entry.Id;
            entryModel.IdVehicle = entry.IdVehicle;
            entryModel.RateTotalValue = GetRateService(entry, departureTime);

            _context.Departures.Add(entryModel);
            return await _context.SaveChangesAsync();
        }

        private decimal GetRateService(Entry entry, DateTime departureDate)
        {
            var serviceTotalTime = departureDate - entry.EntryTime;
            var serviceHours = serviceTotalTime.Hours == 0 ? 1 : serviceTotalTime.Hours;
            var rate = _context.Rates.Where(r => r.IdVehicleType == entry.IdVehicleType).FirstOrDefault();
            var isParsed = int.TryParse(entry.CC, out int CCVehicle);
            var surchargeHour = 0;
            decimal totalRate = 0;

            if(!isParsed)
            {
                throw new DepartureException("The CC could not be readed");
            }

            if(entry.IdVehicleType == (int)VehicleTypeEnum.motorcycle && CCVehicle >= CCLimitSurcharge)
            {
                surchargeHour += 2000;
            }

            if(serviceHours < day)
            {
                totalRate = serviceHours * rate.HourValue;
            }

            if(serviceHours > day)
            {
                if (serviceHours <= dayhours)
                {
                    totalRate = rate.DayValue;
                }

                if (serviceHours > dayhours)
                {
                    totalRate = rate.DayValue;
                    var hoursLeft = dayhours - serviceHours;
                    totalRate += hoursLeft * rate.HourValue;
                }

            }

            return totalRate + surchargeHour;
        }
    }
}
