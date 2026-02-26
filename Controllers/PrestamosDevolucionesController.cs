using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thebook.DTOs;
using Thebook.Exceptions;
using Thebook.Repository;
using Thebook.Services;

namespace Thebook.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]
    public class PrestamosDevolucionesController : ControllerBase
    {
        private readonly IPrestamoService _prestamoService;
        private readonly IDevolucionService _devolucionService;
        private readonly IValidator<PrestamoInsertDto> _prestamoInsertValidator;

        public PrestamosDevolucionesController(IPrestamoService prestamoService, IDevolucionService devolucionService,
                                               IValidator<PrestamoInsertDto> prestamoInsertValidator)
        {
            _prestamoService = prestamoService;
            _devolucionService = devolucionService;
            _prestamoInsertValidator = prestamoInsertValidator;
        }

        [HttpPost]
        public async Task<ActionResult<PrestamoGetDto>> Add(PrestamoInsertDto prestamoInsertDto)
        {
            var validarPrestamo = await _prestamoInsertValidator.ValidateAsync(prestamoInsertDto);
            if (!validarPrestamo.IsValid)
                return BadRequest(validarPrestamo.Errors);

            try
            {
                var prestamoDto = await _prestamoService.Add(prestamoInsertDto);
                return Ok(prestamoDto);
            }
            catch (BusinessException ex)
            {
                return BadRequest(new { error = ex.Message });
            }                        
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PrestamoGetDto>> Devolucion(int id)
        {
            try
            {
                var devolucion = await _devolucionService.UpdateDevolucion(id);

                if (!devolucion.Success)
                    return BadRequest(devolucion.Error);

                return Ok(devolucion);
            }
            catch (BusinessException ex)
            {
                
                return BadRequest(new { error = ex.Message });                
            }
        }

    }
}
