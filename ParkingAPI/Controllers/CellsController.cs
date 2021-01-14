using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParkingAPI.Entities;
using ParkingAPI.Exceptions;
using ParkingAPI.Services.Interfaces;

namespace ParkingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CellsController : ControllerBase
    {
        private readonly ICellService _cellService;

        public CellsController(ICellService cellService)
        {
            _cellService = cellService;
        }

        // GET: api/Cells
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cell>>> GetCells()
        {
            var cells = await _cellService.GetCells();
            return Ok(cells);
        }

        //GET: api/Cells/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cell>> GetCell(int id)
        {
            var cell = await _cellService.GetCellById(id);

            if (cell == null)
            {
                return NotFound();
            }

            return cell;
        }

        // PUT: api/Cells/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCell(int id, Cell cell)
        {
            try
            {
                await _cellService.PutCell(id, cell);
            }
            catch(CellException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (System.Exception)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/Cells
        [HttpPost]
        public async Task<ActionResult<Cell>> PostCell(Cell cell)
        {
            bool exists = _cellService.CellExists(cell.Id);
            if(!exists)
            {
                var added = await _cellService.AddCell(cell);
                return CreatedAtAction("GetCell", new { id = cell.Id }, added);
            } else
            {
                return Conflict(new { message = $"An existing record with the vehicle type '{cell.IdVehicleType}' was already found." });
            }
        }

        // DELETE: api/Cells/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cell>> DeleteCell(int id)
        {
            Cell cell = new Cell(); 
            try
            {
                cell = await _cellService.DeleteCell(id);
            }
            catch(KeyNotFoundException exception)
            {
                NotFound(exception.Message);
            }
            catch (System.Exception)
            {
                throw;
            }

            return cell;
        }
    }
}
