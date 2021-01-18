using AppCore.Exceptions;
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApp.Mappers;
using WebApp.Models;

namespace WebApp.Controllers
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

        //GET: api/Departure
        [HttpGet]
        public ActionResult<IEnumerable<DTODeparture>> GetDepartures()
        {
            var cells = _departureService.GetDepartures();
            return Ok(cells);
        }

        //GET: api/Departure/xx
        [HttpGet("{id}")]
        public ActionResult<DTODeparture> GetDeparture(string id)
        {
            var cell = DepartureMapper.convertDTOToModel(_departureService.GetEntryById(id));

            if (cell == null)
            {
                return NotFound();
            }

            return Ok(cell);
        }

        //POST: api/Departure
        [HttpPost]
        public ActionResult<IEnumerable<DTODeparture>> SaveDeparture(DepartureModel departure)
        {
            DTODeparture added = new DTODeparture();
            if (ModelState.IsValid)
            {
                try
                {
                    added = _departureService.RegistryDeparture(DepartureMapper.convertModelToDTO(departure));
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

            return CreatedAtAction(nameof(GetDeparture), new { id = added.Id }, added);
        }
    }
}
