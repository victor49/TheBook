using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thebook.DTOs;
using Thebook.Services;

namespace Thebook.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]
    public class LibroController : ControllerBase
    {
        private readonly ILibroService _libroService;

        public LibroController(ILibroService libroService)
        {
            _libroService = libroService;
        }

        [HttpGet]
        public async Task<IEnumerable<LibroGetDto>> Get()
            => await _libroService.Get();

        [HttpGet("{titulo}")]
        public async Task<ActionResult<LibroGetDto>> GetByTitulo(string titulo)
        {
            var libroDto = await _libroService.GetByTitulo(titulo);

            return libroDto == null ? NotFound() : Ok(libroDto);
        }

        [HttpPost]
        public async Task<ActionResult<LibroGetDto>> Add(LibroInsertDto libroInsertDto)
        {
            var libroDto =await _libroService.Add(libroInsertDto);
            return libroDto == null ? NotFound() : Ok(libroInsertDto);
        }

        [HttpPut("{titulo}")]
        public async Task<ActionResult<LibroGetDto>> Update(string titulo, LibroUpdateDto libroUpdateDto)
        {
            var libroDto = await _libroService.Update(titulo, libroUpdateDto);

            return libroDto == null ? NotFound() : Ok(libroDto);
        }

        [HttpDelete("{titulo}")]
        public async Task<ActionResult<LibroGetDto>> Delete(string titulo)
        {
            var libroDto = await _libroService.Delete(titulo);

            return libroDto == null ? NotFound() : Ok(libroDto);
        }
    }
}
