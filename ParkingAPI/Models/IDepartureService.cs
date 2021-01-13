using ParkingAPI.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingAPI.Models
{
    public interface IDepartureService
    {
        Task<List<DepartureDTO>> GetDepartures();

        Task<DepartureDTO> GetCellByIdEntry(int id);

        Task<int> SetDeparture(DepartureDTO idVehicle);
    }
}
