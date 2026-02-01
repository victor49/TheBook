using Thebook.DTOs;
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
            var libroExiste = await _libroRepository.ExsiteLibro(prestamoInsertDto.IdLibro);
            var usurioExiste = await _usuarioRepository.ExisteUsuario(prestamoInsertDto.IdUsuario);

            var cantidadLibro = await _libroRepository.CantidaLibro(prestamoInsertDto.IdLibro);

            if(libroExiste && usurioExiste && cantidadLibro.CantidadDisponible > 0)
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
                await _libroService.UpdateCantidaLibro(prestamoInsertDto.IdLibro);

                var prestamoDto = new PrestamoGetDto
                {
                    IdPrestamo = prestamo.IdPrestamo,
                    FechaPrestamo = prestamo.FechaPrestamo,
                    FechaDevolucionEstimada = prestamo.FechaDevolucionEstimada
                    //Usuario = prestamo.IdUsuario,
                };
                return prestamoDto;
            }
            return null;
        }
    }
}
