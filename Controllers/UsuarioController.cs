using FluentValidation;
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
        private readonly IValidator<UsuarioInsertDto> _usuarioInsertValidator;
        private readonly IValidator<UsuarioUpdateDto> _usuarioUpdateValidator;

        public UsuarioController(IUsuarioService usuarioService, IValidator<UsuarioInsertDto> usuarioInsertValidator,
                                 IValidator<UsuarioUpdateDto> usuarioUpdateValidator)
        {
            _usuarioService = usuarioService;
            _usuarioInsertValidator = usuarioInsertValidator;
            _usuarioUpdateValidator = usuarioUpdateValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<UsuarioGetDto>> Get() 
            => await _usuarioService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioGetDto>> GetById(int id)
        {
           
            var usuarioDto = await _usuarioService.GetById(id);
            return usuarioDto;
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioGetDto>> Add(UsuarioInsertDto usuarioInsertDto)
        {
            var validarUsuario = await _usuarioInsertValidator.ValidateAsync(usuarioInsertDto);
            if (!validarUsuario.IsValid)
                return BadRequest(validarUsuario.Errors);

            var usuarioDto = await _usuarioService.Add(usuarioInsertDto);
            return usuarioDto == null ? NotFound() : Ok(usuarioDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioGetDto>> Update(int id, UsuarioUpdateDto usuarioUpdateDto)
        {
            var validarUsuario = await _usuarioUpdateValidator.ValidateAsync(usuarioUpdateDto);
            if (!validarUsuario.IsValid)
                return BadRequest(validarUsuario.Errors);
            
            var usuaroDto = await _usuarioService.Update(id, usuarioUpdateDto);
            return Ok(usuaroDto);
    }

    }
}
