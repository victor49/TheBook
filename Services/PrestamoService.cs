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

        public PrestamoService(IPrestamoRepository prestamoRepository, ILibroRepository libroRepository, 
            IUsuarioRepository usuarioRepository, ILibroService libroService)
        {
            _prestamoRepository = prestamoRepository;
            _libroRepository = libroRepository;
            _usuarioRepository = usuarioRepository;
            _libroService = libroService;
        }

        public async Task<PrestamoGetDto> Add(PrestamoInsertDto prestamoInsertDto)
        {
            //Comprobar si existe en la db el usuario o libro 
            var libro = await _libroRepository.GetById(prestamoInsertDto.IdLibro);
            var usuario = await _usuarioRepository.GetById(prestamoInsertDto.IdUsuario);

            if (libro == null)
                throw new BusinessException("Libro no Existe");

            else if (libro.CantidadDisponible == 0)
                throw new BusinessException("No hay libros diponibles");

            else if (usuario == null)
                throw new BusinessException("Usuario no existe");

            else
            {
                var prestamo = new Prestamo()
                {
                    FechaPrestamo = DateOnly.FromDateTime(DateTime.Now),
                    FechaDevolucionEstimada = prestamoInsertDto.FechaDevolucionEstimada,
                    IdLibro = prestamoInsertDto.IdLibro,
                    IdUsuario = prestamoInsertDto.IdUsuario
                }; 
                await _prestamoRepository.Add(prestamo);
                await _prestamoRepository.Save();
                
                //Disminuir cantidiad de libros
                await _libroService.UpdateCantidaLibroDisminuir(prestamoInsertDto.IdLibro);

                var prestamoDto = new PrestamoGetDto
                {
                    IdPrestamo = prestamo.IdPrestamo,
                    FechaPrestamo = prestamo.FechaPrestamo,
                    FechaDevolucionEstimada = prestamo.FechaDevolucionEstimada                   
                };
                return prestamoDto;
            }
        }
    }
}
