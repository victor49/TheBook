using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thebook.DTOs;
using Thebook.Services;

namespace Thebook.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleadoService _empleadoService;

        public EmpleadoController(IEmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmpleadoGetDto>> GetById(int id)
        {
            var empleadoDto = await _empleadoService.GetById(id);
            return empleadoDto == null ? NotFound() : Ok(empleadoDto);
        }

        [HttpPost]
        public async Task<ActionResult<EmpleadoDto>> Add(EmpleadoInsertDto empleadoInsertDto)
        {
            var empleadoDto = await _empleadoService.Add(empleadoInsertDto);
            return empleadoDto == null ? NotFound() : Ok(empleadoDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmpleadoDto>> Update(int id, EmpleadoUpdateDto empleadoUpdateDto)
        {
            var empleadoDto = await _empleadoService.Update(id, empleadoUpdateDto);

            return empleadoDto == null ? NotFound() : Ok(empleadoDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<EmpleadoDto>> Delete(int id)
        {
            var empleadoDto = await _empleadoService.Delete(id);
            return empleadoDto == null ? NotFound() : Ok(empleadoDto);
        }
    }
}
