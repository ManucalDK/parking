using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingAPI.Models
{
    public interface ICellService
    {
        Task<List<Cell>> GetCells();

        Task<int> AddCell(Cell cell);

        Task<Cell> GetCellById(int id);

        bool CellExists(int id);

        Task<Cell> DeleteCell(int id);

        Task<bool> PutCell(int id, Cell cell);

        Cell GetCellByVehicleType(int vehicleType);
    }
}
