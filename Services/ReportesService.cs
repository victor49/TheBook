using Thebook.DTOs;
using Thebook.Exceptions;
using Thebook.Models;
using Thebook.Repository;

namespace Thebook.Services
{
    public class ReportesService : IReportesService
    {
        private readonly IPrestamoRepository _prestamoRepository;
        private readonly ILibroRepository _libroRepository;

        public ReportesService(IPrestamoRepository prestamoRepository, ILibroRepository libroRepository)
        {
            _prestamoRepository = prestamoRepository;
            _libroRepository = libroRepository;
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

        public async Task<IEnumerable<PrestamoGetDto>> GetPrestamosPorUsuario(int idUsuario)
        {
            var prestamos = await _prestamoRepository.GetPrestamosPorUsuario(idUsuario);

            return prestamos.Select(p => new PrestamoGetDto
            {
                IdPrestamo = p.IdPrestamo,
                FechaPrestamo = p.FechaPrestamo,
                FechaDevolucionEstimada = p.FechaDevolucionEstimada,
                FechaDevolucionReal = p.FechaDevolucionReal
            });
        }

        public async Task<LibroGetDto> LibroMasPrestado()
        {
            var idLibroMasPrestado = await _prestamoRepository.LibroMasPrestado();

            var libro = await _libroRepository.GetById(idLibroMasPrestado);

            if (libro  != null)
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
        public async Task<IEnumerable<LibroGetDto>> GetLibrosNoDisponibles()
        {
            var librosNoDisponibles = await _libroRepository.GetLibrosNoDisponibles();

            return librosNoDisponibles.Select(l => new LibroGetDto
            {
                IdLibro = l.IdLibro,
                Titulo = l.Titulo,
                Autor = l.Autor,
                Editorial = l.Editorial,
                Categoria = l.Categoria,
                CantidadDisponible = l.CantidadDisponible
            });                                                 
        }
    }
}
