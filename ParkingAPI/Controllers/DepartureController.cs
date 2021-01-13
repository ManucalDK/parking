using Microsoft.AspNetCore.Mvc;
using ParkingAPI.DTO;
using ParkingAPI.Exceptions;
using ParkingAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingAPI.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class DepartureController : ControllerBase
    {
        private readonly IDepartureService _departureService;
        public DepartureController(IDepartureService departureService)
        {
            _departureService = departureService;
        }

        // GET: api/Departure
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartureDTO>>> GetDepartures()
        {
            var cells = await _departureService.GetDepartures();
            return Ok(cells);
        }

        [HttpPost]
        public async Task<IActionResult> SetDeparture(DepartureDTO departure)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _departureService.SetDeparture(departure);
                }
                catch (Exception e)
                {
                    if (e is DepartureException)
                    {
                        return UnprocessableEntity(e.Message);
                    }
                    else
                    {
                        return BadRequest(e.Message);
                    }
                }
            }
            return Ok();
        }
    }
}
