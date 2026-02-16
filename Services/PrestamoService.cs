using AutoMapper;
using Thebook.DTOs;
using Thebook.Exceptions;
using Thebook.Models;
using Thebook.Repository;

namespace Thebook.Services
{
    public class PrestamoService : IPrestamoService
    {
        private readonly IPrestamoRepository _prestamoRepository;
        private readonly ILibroRepository _libroRepository;
        private readonly IUsuarioRepository  _usuarioRepository;
        private readonly ILibroService _libroService;
        private readonly IMapper _mapper;

        public PrestamoService(IPrestamoRepository prestamoRepository, ILibroRepository libroRepository, 
            IUsuarioRepository usuarioRepository, ILibroService libroService, IMapper mapper)
        {
            _prestamoRepository = prestamoRepository;
            _libroRepository = libroRepository;
            _usuarioRepository = usuarioRepository;
            _libroService = libroService;
            _mapper = mapper;
        }

        public async Task<PrestamoGetDto> Add(PrestamoInsertDto prestamoInsertDto)
        {
            //Comprobar si existe en la db el usuario y libro 
            var libro = await _libroRepository.GetById(prestamoInsertDto.IdLibro);
            var usuario = await _usuarioRepository.GetById(prestamoInsertDto.IdUsuario);

            //Validar que el usuario no supere mas de 3 libros prestados 
            var prestamosActivos = await _prestamoRepository.CountPrestamosActivosByUsuario(prestamoInsertDto.IdUsuario);

            if (libro == null)
                throw new BusinessException("Libro no Existe");

            else if (libro.CantidadDisponible == 0)
                throw new BusinessException("No hay libros diponibles");

            else if (usuario == null)
                throw new BusinessException("Usuario no existe");

            else if (prestamosActivos >= 3)
                throw new BusinessException("El usuario ya tiene el máximo de 3 libros prestados");

            else
            {
                var prestamo = _mapper.Map<Prestamo>(prestamoInsertDto);
                
                prestamo.FechaPrestamo = DateOnly.FromDateTime(DateTime.Now);

                await _prestamoRepository.Add(prestamo);
                await _prestamoRepository.Save();

                //Disminuir cantidiad de libros
                await _libroService.UpdateCantidaLibroDisminuir(prestamoInsertDto.IdLibro);

                var prestamoDto = _mapper.Map<PrestamoGetDto>(prestamo);
                return prestamoDto;
            }
        }
    }
}
