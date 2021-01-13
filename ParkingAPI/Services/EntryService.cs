using ParkingAPI.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using ParkingAPI.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Entities.Models;
using ParkingAPI.Models;

namespace ParkingAPI.Services
{
    public partial class EntryService : IEntryService
    {
        private readonly ParkingContext _context;
        private readonly ICellService _cellService;
        private readonly IDepartureService _departureService;


        public EntryService(ParkingContext context, ICellService cellService, IDepartureService departureService)
        {
            _context = context;
            _cellService = cellService;
            _departureService = departureService;
        }

        public async Task<List<EntryDTO>> GetEntries()
        {
            var entriesResult = await _context.Entries.Select(b =>
                                      new EntryDTO()
                                      {
                                          CC = b.CC,
                                          EntryTime = b.EntryTime,
                                          IdVehicle = b.IdVehicle,
                                          IdVehicleType = b.IdVehicleType
                                      }).ToListAsync();
            return entriesResult;
        }

        public async Task<int> RegistryVehicle(EntryDTO entry)
        {
            var lastEntryByIdVehicle = await _context.Entries.Where(e => e.IdVehicle == entry.IdVehicle).LastOrDefaultAsync();

            if (lastEntryByIdVehicle != null)
            {
                var departure = await _departureService.GetCellByIdEntry(lastEntryByIdVehicle.Id);
                if (departure == null)
                {
                    throw new EntryException("The vehicle has an entry without a departure. Wait...how?");
                }
            }

            if (!ExistsQuota(entry.IdVehicleType))
            {
                throw new NotQuotaException("It doesn't exists quotas");
            }


            var lastNumberIdVehicle = GetLastNumberOfIdVehicle(entry.IdVehicleType, entry.IdVehicle);
            bool isParsed = short.TryParse(lastNumberIdVehicle, out short numberResult);

            if (!isParsed)
            {
                throw new EntryException("The vehicle id can't be readed");
            }
            

            if (HasPicoPlaca((int)DateTime.Now.DayOfWeek, numberResult))
            {
                throw new PicoPlacaException("The vehicle has pico placa");
            }

            if ((entry.IdVehicleType == (int)VehicleTypeEnum.motorcycle) && string.IsNullOrEmpty(entry.CC))
            {
                throw new CCException("The vehicle is a motorcycle. The CC is missing");
            }

            Entry entryModel = new Entry();
            var lastEntry = await _context.Entries.LastOrDefaultAsync();
            entryModel.Id = lastEntry != null ? lastEntry.Id + 1 : 1;
            entryModel.CC = entry.CC;
            entryModel.EntryTime = DateTime.Now;
            entryModel.IdVehicle = entry.IdVehicle;
            entryModel.IdVehicleType = entry.IdVehicleType;


            _context.Add(entryModel);
            return await _context.SaveChangesAsync();
        }

        public bool ExistsQuota(int vehicleType)
        {
            int avaliableCells = _cellService.GetCellByVehicleType(vehicleType).NumCellAvaliable;
            int busyPlaces = GetEntriesByVehicleType(vehicleType);

            return busyPlaces < avaliableCells;
        }

        public int GetEntriesByVehicleType(int vehicleType)
        {
            return _context.Entries.Where(entrie => entrie.IdVehicleType == vehicleType).Count();
        }

        private bool HasPicoPlaca(int day, int vehicleLastNumberId)
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

        private string GetLastNumberOfIdVehicle(int vehicleTypeId, string vehicleId)
        {
            string lastNumber = string.Empty;
            if(vehicleTypeId == (int)VehicleTypeEnum.car)
            {
                lastNumber = vehicleId.Last().ToString();
            }

            if (vehicleTypeId == (int)VehicleTypeEnum.motorcycle)
            {
                lastNumber = vehicleId.TakeLast(2).FirstOrDefault().ToString();
            }

            return lastNumber;

        }
    }
}
