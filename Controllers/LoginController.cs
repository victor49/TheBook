using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto empleado)
        {
            string token = await _loginService.GetEmpleado(empleado.Email, empleado.Password);

            if (token == null)
                return Unauthorized("Credenciales incorrectas");

            return Ok(new{ token});
        }
    }
}
