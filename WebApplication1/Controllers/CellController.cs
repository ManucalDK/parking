using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CellController : ControllerBase
    {
        private readonly ICellService _cellService;
        public CellController(ICellService entryService)
        {
            _cellService = entryService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DtoCell>> GetCells()
        {
            return Ok();
        }
    }
}
