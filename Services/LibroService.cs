using AutoMapper;
using Thebook.DTOs;
using Thebook.Exceptions;
using Thebook.Models;
using Thebook.Repository;

namespace Thebook.Services
{
    public class LibroService : ILibroService
    {
        private readonly ILibroRepository _libroRepository;
        private readonly IMapper _mapper;

        private static string nombreModelo = "Libro";
        public LibroService(ILibroRepository libroRepository, IMapper mapper)
        {
            _libroRepository = libroRepository;
            _mapper = mapper;
        }        

        public async Task<IEnumerable<LibroGetDto>> Get()
        {
            var libros = await _libroRepository.Get();

            return libros.Select(l => _mapper.Map<LibroGetDto>(l));
        }

        public async Task<LibroGetDto> GetByTitulo(string titulo)
        {
            var libro = await _libroRepository.GetByTitle(titulo);

            if (libro == null)
                throw new NotFoundException(titulo);

            else
            {
                var libroDto = _mapper.Map<LibroGetDto>(libro);
                return libroDto;
            }
        }

        public async Task<LibroGetDto> Add(LibroInsertDto libroInsertDto)
        {
            var libro = _mapper.Map<Libro>(libroInsertDto);

            await _libroRepository.Add(libro);
            await _libroRepository.Save();

            var tareaDto = _mapper.Map<LibroGetDto>(libro);
            return tareaDto;
        }

        public async Task<LibroGetDto> Update(string Titulo, LibroUpdateDto libroUpdateDto)
        {
            var libro = await _libroRepository.GetByTitle(Titulo);

            if (libro == null)
                throw new NotFoundException(Titulo);

            else
            {
                libro = _mapper.Map(libroUpdateDto, libro);

                _libroRepository.Update(libro);
                await _libroRepository.Save();

                var libroDto = _mapper.Map<LibroGetDto>(libro);
                return libroDto;
            }
        }

        public async Task<LibroGetDto> Delete(string titulo)
        {
            var libro = await _libroRepository.GetByTitle(titulo);

            if (libro == null) 
                throw new NotFoundException(titulo);

            else
            {
                var libroDto = _mapper.Map<LibroGetDto>(libro);

                _libroRepository.Delete(libro);
                await _libroRepository.Save();

                return libroDto;
            }
        }

        public async Task UpdateCantidaLibroDisminuir(int id)
        {
            var libro = await _libroRepository.GetById(id);
            
            if (libro == null)
                throw new NotFoundException(nombreModelo, id);

            else
            {
                libro.CantidadDisponible--;

                _libroRepository.Update(libro);
                await _libroRepository.Save();               
            }            
        }

        public async Task UpdateCantidaLibroAumentar(int id)
        {
            var libro = await _libroRepository.GetById(id);

            if (libro == null)
                throw new NotFoundException(nombreModelo, id);

            else
            {
                libro.CantidadDisponible++;
                _libroRepository.Update(libro);
                await _libroRepository.Save();
            }
        }
    }
}
