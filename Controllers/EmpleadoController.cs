using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thebook.DTOs;
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
            var empleadoDto = await _empleadoService.GetById(id);
            return Ok(empleadoDto);            
        }

        [HttpPost]
        public async Task<ActionResult<LoginRequestDto>> Add(EmpleadoInsertDto empleadoInsertDto)
        {
            var validacionEmpleado = await _empleadoInsertValidator.ValidateAsync(empleadoInsertDto);
            if (!validacionEmpleado.IsValid)
                return BadRequest(validacionEmpleado.Errors);

            var empleadoDto = await _empleadoService.Add(empleadoInsertDto);
            return empleadoDto == null ? NotFound() : Ok(empleadoDto);                                  
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<LoginRequestDto>> Update(int id, EmpleadoUpdateDto empleadoUpdateDto)
        {
            var validacionEmpleado = await _empleadoUpdateValidator.ValidateAsync(empleadoUpdateDto);
            if (!validacionEmpleado.IsValid)
                return BadRequest(validacionEmpleado.Errors);
           
            var empleadoDto = await _empleadoService.Update(id, empleadoUpdateDto);
            return Ok(empleadoDto);           
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<LoginRequestDto>> Delete(int id)
        {        
            var empleadoDto = await _empleadoService.Delete(id);
            return Ok(empleadoDto);            
        }
    }
}
