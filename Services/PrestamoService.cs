using Thebook.DTOs;
using Thebook.Models;
using Thebook.Repository;

namespace Thebook.Services
{
    public class PrestamoService : IPrestamoService
    {
        private readonly IPrestamoRepository _prestamoRepository;
        private readonly ILibroRepository _libroRepository;

        public PrestamoService(IPrestamoRepository prestamoRepository, ILibroRepository libroRepository)
        {
            _prestamoRepository = prestamoRepository;
            _libroRepository = libroRepository;
        }

        public async Task<PrestamoGetDto> Add(PrestamoInsertDto prestamoInsertDto)
        {
            var libroExiste = await _libroRepository.ExsiteLibro(prestamoInsertDto.IdLibro);

            if(libroExiste)
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
