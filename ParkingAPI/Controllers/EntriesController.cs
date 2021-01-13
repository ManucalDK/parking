using ParkingAPI.DTO;
using ParkingAPI.Exceptions;
using ParkingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EntriesController : ControllerBase
    {
        private readonly IEntryService _entryService;

        public EntriesController(IEntryService context)
        {
            _entryService = context;
        }

        // GET: api/Entries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EntryDTO>>> GetEntries()
        {
            var cells = await _entryService.GetEntries();
            return Ok(cells);
        }

        // POST: api/Entries
        public async Task<IActionResult> Create(EntryDTO entry)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _entryService.RegistryVehicle(entry);
                }
                catch (Exception e)
                {
                    if (e is NotConfiguredException || e is NotQuotaException || e is PicoPlacaException || e is CCException || e is EntryException)
                    {
                        return UnprocessableEntity(e.Message);
                    } else
                    {
                        return BadRequest(e.Message);
                    }
                }
            }
            return Ok();
        }
    }
}
