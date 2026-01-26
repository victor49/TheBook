using Thebook.DTOs;
using Thebook.Services;

namespace Thebook.Repository
{
    public class LibroService : ILibroService
    {
        private readonly ILibroRepository _libroRepository;

        public LibroService(ILibroRepository libroRepository)
        {
            _libroRepository = libroRepository;
        }

        public async Task<IEnumerable<LibroGetDto>> Get()
        {
            var libros = await _libroRepository.Get();

            return libros.Select(l => new LibroGetDto
            {
                IdLibro = l.IdLibro,
                Titulo = l.Titulo,
                Autor = l.Autor,
                Editorial = l.Editorial,
                Categoria = l.Categoria,
                CantidadDisponible = l.CantidadDisponible
            });
        }

        public async Task<LibroGetDto> GetByTitulo(string titulo)
        {
            var libro = await _libroRepository.GetByTitle(titulo);

            if (libro != null)
            {
                var libroDto = new LibroGetDto
                {
                    IdLibro = libro.IdLibro,
                    Titulo = libro.Titulo,
                    Autor = libro.Autor,
                    Editorial = libro.Editorial,
                    Categoria = libro.Categoria,
                    CantidadDisponible = libro.CantidadDisponible
                };
                return libroDto;
            }
            return null;
        }        
    }
}
