using Thebook.DTOs;
using Thebook.Models;
using Thebook.Repository;

namespace Thebook.Services
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

        public async Task<LibroGetDto> Add(LibroInsertDto libroInsertDto)
        {
            var libro = new Libro()
            {
                Titulo = libroInsertDto.Titulo,
                Autor = libroInsertDto.Autor,
                Editorial = libroInsertDto.Editorial,
                Categoria= libroInsertDto.Categoria,
                CantidadDisponible = libroInsertDto.CantidadDisponible
            };

            await _libroRepository.Add(libro);
            await _libroRepository.Save();

            var tareaDto = new LibroGetDto
            {
                IdLibro = libro.IdLibro,
                Titulo = libro.Titulo,
                Autor = libro.Autor,
                Editorial = libro.Editorial,
                Categoria = libro.Categoria,
                CantidadDisponible = libro.CantidadDisponible
            };
            return tareaDto;
        }

        public async Task<LibroGetDto> Update(string Titulo, LibroUpdateDto libroUpdateDto)
        {
            var libro = await _libroRepository.GetByTitle(Titulo);

            if (libro !=  null)
            {
                libro.Titulo = libroUpdateDto.Titulo;
                libro.Autor = libroUpdateDto.Autor;
                libro.Editorial = libroUpdateDto.Editorial;
                libro.Categoria = libroUpdateDto.Categoria;
                libro.CantidadDisponible = libroUpdateDto.CantidadDisponible;

                _libroRepository.Update(libro);
                await _libroRepository.Save();

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

        public async Task<LibroGetDto> Delete(string titulo)
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

                _libroRepository.Delete(libro);
                await _libroRepository.Save();

                return libroDto;
            }
            return null;
        }

        public async Task UpdateCantidaLibroDisminuir(int id)
        {
            var libro = await _libroRepository.GetById(id);

            if (libro != null)
            {
                libro.CantidadDisponible--;

                _libroRepository.Update(libro);
                await _libroRepository.Save();               
            }            
        }

        public async Task UpdateCantidaLibroAumentar(int id)
        {
            var libro = await _libroRepository.GetById(id);

            if(libro != null)
            {
                libro.CantidadDisponible++;
                _libroRepository.Update(libro);
                await _libroRepository.Save();
            }
        }
    }
}
