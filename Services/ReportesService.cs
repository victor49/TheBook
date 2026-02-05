using Thebook.DTOs;
using Thebook.Exceptions;
using Thebook.Models;
using Thebook.Repository;

namespace Thebook.Services
{
    public class ReportesService : IReportesService
    {
        private readonly IPrestamoRepository _prestamoRepository;

        public ReportesService(IPrestamoRepository prestamoRepository)
        {
            _prestamoRepository = prestamoRepository;
        }

        public async Task<IEnumerable<PrestamoGetDto>> GetPrestamosActivos()
        {
            var prestamosActivos = await _prestamoRepository.GetPrestamosActivos();

            return prestamosActivos.Select(p => new PrestamoGetDto
            {
                IdPrestamo = p.IdPrestamo,
                FechaPrestamo = p.FechaPrestamo,
                FechaDevolucionEstimada = p.FechaDevolucionEstimada,

                Usuario = new UsuarioGetDto
                {
                    IdUsuario = p.Usuarios.IdUsuario,
                    Nombre = p.Usuarios.Nombre,
                    Correo = p.Usuarios.Correo
                },

                Libro = new LibroGetDto
                {
                    IdLibro = p.Libros.IdLibro,
                    Titulo = p.Libros.Titulo                    
                }
            });
        }
    }
}
