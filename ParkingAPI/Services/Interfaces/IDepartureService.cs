using ParkingAPI.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingAPI.Services.Interfaces
{
    public interface IDepartureService
    {
        Task<List<DepartureDTO>> GetDepartures();

        Task<DepartureDTO> GetCellByIdEntry(int id);

        Task<int> SetDeparture(DepartureDTO idVehicle);
    }
}
