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
    public class EntryController : ControllerBase
    {
        private readonly IEntryService _entryService;

        public EntryController(IEntryService entryService)
        {
            _entryService = entryService;
        }

        //GET: api/Entry
        [HttpGet]
        public ActionResult<IEnumerable<DtoEntry>> GetEntries()
        {
            var cells = _entryService.GetEntries();
            return Ok(cells);
        }

        //GET: api/Entry/xx
        [HttpGet("{id}")]
        public ActionResult<DtoEntry> GetEntry(string id)
        {
            var cell = EntryMapper.convertDTOToModel(_entryService.GetEntryById(id));

            if (cell == null)
            {
                return NotFound();
            }

            return Ok(cell);
        }

        //POST: api/Entry
        [HttpPost]
        public ActionResult<IEnumerable<DtoEntry>> SaveEntry(EntryModel entry)
        {
            DtoEntry added = new DtoEntry();
            if (ModelState.IsValid)
            {
                try
                {
                    added = _entryService.RegistryVehicle(EntryMapper.convertModelToDTO(entry));
                }
                catch (Exception e)
                {
                    if (e is CellException || e is EntryException || e is PlacaException)
                    {
                        return UnprocessableEntity(e.Message);
                    }
                    else
                    {
                        return BadRequest(e.Message);
                    }
                }
            }

            return CreatedAtAction(nameof(GetEntry), new { id = added.Id }, added);
        }

    }
}
