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
    public class LibroController : ControllerBase
    {
        private readonly ILibroService _libroService;
        private readonly IValidator<LibroInsertDto> _libroInsertValidator;
        private readonly IValidator<LibroUpdateDto> _libroUpdateValidator;

        public LibroController(ILibroService libroService, IValidator<LibroInsertDto> libroInsertValidator, 
                               IValidator<LibroUpdateDto> libroUpdateValidator)
        {
            _libroService = libroService;
            _libroInsertValidator = libroInsertValidator;
            _libroUpdateValidator = libroUpdateValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<LibroGetDto>> Get()
            => await _libroService.Get();

        [HttpGet("{titulo}")]
        public async Task<ActionResult<LibroGetDto>> GetByTitulo(string titulo)
        {
            try
            {
                var libroDto = await _libroService.GetByTitulo(titulo);
                return Ok(libroDto);
            }

            catch (NotFoundException ex)
            {
                return NotFound(new {error = ex.Message});
            }
        }

        [HttpPost]
        public async Task<ActionResult<LibroGetDto>> Add(LibroInsertDto libroInsertDto)
        {
            var validarLibro = await _libroInsertValidator.ValidateAsync(libroInsertDto);
            if (!validarLibro.IsValid)
                return BadRequest(validarLibro.Errors);

            var libroDto =await _libroService.Add(libroInsertDto);
            return libroDto == null ? NotFound() : Ok(libroInsertDto);
        }

        [HttpPut("{titulo}")]
        public async Task<ActionResult<LibroGetDto>> Update(string titulo, LibroUpdateDto libroUpdateDto)
        {
            var validarLibro = await _libroUpdateValidator.ValidateAsync(libroUpdateDto);
            if (!validarLibro.IsValid)
                return BadRequest(validarLibro.Errors);

            try
            {
                var libroDto = await _libroService.Update(titulo, libroUpdateDto);
                return Ok(libroDto);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new {error = ex.Message});
            }
        }

        [HttpDelete("{titulo}")]
        public async Task<ActionResult<LibroGetDto>> Delete(string titulo)
        {
            try
            {
                var libroDto = await _libroService.Delete(titulo);
                return Ok(libroDto);
            }
            catch (NotFoundException ex) 
            {
                return NotFound(new {error = ex.Message});
            }
        }
    }
}
