using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thebook.DTOs;
using Thebook.Services;

namespace Thebook.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]
    public class EmpleadoController : Controller
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
    }
}
