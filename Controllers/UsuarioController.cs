using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thebook.DTOs;
using Thebook.Services;

namespace Thebook.Controllers
{
    [Authorize(Roles ="Admin")]
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IEnumerable<UsuarioGetDto>> Get() 
            => await _usuarioService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioGetDto>> GetById(int id)
        {
            var usuarioDto = await _usuarioService.GetById(id);

            return usuarioDto == null ? NotFound() : Ok(usuarioDto);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioGetDto>> Add(UsuarioInsertDto usuarioInsertDto)
        {
            var usuarioDto = await _usuarioService.Add(usuarioInsertDto);

            return usuarioDto == null ? NotFound() : Ok(usuarioDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioGetDto>> Update(int id, UsuarioUpdateDto usuarioUpdateDto)
        {
            var usuaroDto = await _usuarioService.Update(id, usuarioUpdateDto);

            return usuaroDto == null ? NotFound() : Ok(usuaroDto);
        }

    }
}
