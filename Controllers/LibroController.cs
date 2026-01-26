using Microsoft.AspNetCore.Mvc;
using Thebook.DTOs;
using Thebook.Services;

namespace Thebook.Controllers
{
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
    }
}
