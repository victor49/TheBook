using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thebook.DTOs;
using Thebook.Exceptions;
using Thebook.Services;

namespace Thebook.Controllers
{
    //[Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleadoService _empleadoService;
        private readonly IValidator<EmpleadoInsertDto> _empleadoInsertValidator;
        private readonly IValidator<EmpleadoUpdateDto> _empleadoUpdateValidator;

        public EmpleadoController(IEmpleadoService empleadoService, IValidator<EmpleadoInsertDto> empleadoInsertValidator,
                                  IValidator<EmpleadoUpdateDto> empleadoUpdateValidator)
        {
            _empleadoService = empleadoService;
            _empleadoInsertValidator = empleadoInsertValidator;
            _empleadoUpdateValidator = empleadoUpdateValidator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmpleadoGetDto>> GetById(int id)
        {
            try
            {
                var empleadoDto = await _empleadoService.GetById(id);
                return Ok(empleadoDto);
            }
            catch (NotFoundException ex)
            {
                return BadRequest(new{error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<EmpleadoDto>> Add(EmpleadoInsertDto empleadoInsertDto)
        {

            var validacionEmpleado = await _empleadoInsertValidator.ValidateAsync(empleadoInsertDto);
            if (!validacionEmpleado.IsValid)
                return BadRequest(validacionEmpleado.Errors);

            try
            {
                var empleadoDto = await _empleadoService.Add(empleadoInsertDto);
                return Ok(empleadoDto);
            }
            catch (NotFoundException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
                                  
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmpleadoDto>> Update(int id, EmpleadoUpdateDto empleadoUpdateDto)
        {
            var validacionEmpleado = await _empleadoUpdateValidator.ValidateAsync(empleadoUpdateDto);
            if (!validacionEmpleado.IsValid)
                return BadRequest(validacionEmpleado.Errors);

            try
            {
                var empleadoDto = await _empleadoService.Update(id, empleadoUpdateDto);
                return Ok(empleadoDto);
            }
            catch (NotFoundException ex)
            {
                return BadRequest(new {error = ex.Message});
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<EmpleadoDto>> Delete(int id)
        {
            try
            {
                var empleadoDto = await _empleadoService.Delete(id);
                return Ok(empleadoDto);
            }
            catch (NotFoundException ex)
            {
                return BadRequest(new {error =ex.Message});
            }
        }
    }
}
