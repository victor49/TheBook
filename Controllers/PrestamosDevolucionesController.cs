using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thebook.DTOs;
using Thebook.Exceptions;
using Thebook.Repository;
using Thebook.Services;

namespace Thebook.Controllers
{
    //[Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]
    public class PrestamosDevolucionesController : ControllerBase
    {
        private readonly IPrestamoService _prestamoService;
        private readonly IDevolucionService _devolucionService;

        public PrestamosDevolucionesController(IPrestamoService prestamoService, IDevolucionService devolucionService)
        {
            _prestamoService = prestamoService;
            _devolucionService = devolucionService;
        }

        //[HttpGet]


        [HttpPost]
        public async Task<ActionResult<PrestamoGetDto>> Add(PrestamoInsertDto prestamoInsertDto)
        {
            try
            {
                var prestamoDto = await _prestamoService.Add(prestamoInsertDto);
                return Ok(prestamoDto);
            }
            catch (BusinessException ex)
            {
                return BadRequest(new {error = ex.Message });
            }                        
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PrestamoGetDto>> Devolucion(int id)
        {
            try
            {
                var devolucion = await _devolucionService.UpdateDevolucion(id);
                return Ok(devolucion);
            }
            catch (BusinessException ex)
            {
                
                return BadRequest(new { error = ex.Message });
                
            }
        }

    }
}
