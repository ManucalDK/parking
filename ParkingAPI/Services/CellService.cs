using ParkingAPI.Data;
using ParkingAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingAPI.Exceptions;
using ParkingAPI.Services.Interfaces;

namespace ParkingAPI.Services
{
    public class CellService : ICellService
    {
        private readonly ParkingContext _context;


        public CellService(ParkingContext context)
        {
            _context = context;
        }


        public async Task<List<Cell>> GetCells()
        {
            var cells = await _context.Cells.ToListAsync();
            return cells;
        }

        public async Task<int> AddCell(Cell cell)
        {
            _context.Add(cell);
            return await _context.SaveChangesAsync();
        }

        public async Task<Cell> GetCellById(int id)
        {
            return await _context.Cells.FindAsync(id);  
        }

        public bool CellExists(int id)
        {
            return _context.Cells.Any(e => e.Id == id);
        }

        public async Task<Cell> DeleteCell(int id)
        {
            var cell = await _context.Cells.FindAsync(id);
            if (cell == null)
            {
                throw new CellException($"The cell with code {id} was not found");
            }

            _context.Cells.Remove(cell);
            await _context.SaveChangesAsync();

            return cell;
        }
        public async Task<bool> PutCell(int id, Cell cell)
        {
            bool updated;

            if (id != cell.Id)
            {
                throw new CellException();
            }

            _context.Entry(cell).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                updated = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CellExists(id))
                {
                    throw new CellException($"The cell with code { id} was not found");
                }
                else
                {
                    throw;
                }
            }

            return updated;
        }

        public Cell GetCellByVehicleType(int vehicleType)
        {
            Cell cellsByVehicleType = _context.Cells.Where(x => x.IdVehicleType == vehicleType).FirstOrDefault();

            if(cellsByVehicleType == null)
            {
                throw new NotConfiguredException("Not exists a cell configured");
            }

            return cellsByVehicleType;
        }
    }
}
