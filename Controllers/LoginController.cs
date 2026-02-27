using Microsoft.AspNetCore.Mvc;
using Thebook.DTOs;
using Thebook.Services;

namespace Thebook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        [HttpGet]
        public async Task<IActionResult> Get(EmpleadoDto empleado)
        {
            string token = await _loginService.GetEmpleado(empleado.Email, empleado.Password);

            if (token == null)
                return NotFound("Credenciales incorrectas");

            return Ok(token);
        }
    }
}
