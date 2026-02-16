using AutoMapper;
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
        private readonly IMapper _mapper;

        public ReportesService(IPrestamoRepository prestamoRepository, ILibroRepository libroRepository, IMapper mapper)
        {
            _prestamoRepository = prestamoRepository;
            _libroRepository = libroRepository;
            _mapper = mapper;
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
                var libroDto = _mapper.Map<LibroGetDto>(libro);
                return libroDto;
            }
            return null;
        }
        public async Task<IEnumerable<LibroGetDto>> GetLibrosNoDisponibles()
        {
            var librosNoDisponibles = await _libroRepository.GetLibrosNoDisponibles();

            return librosNoDisponibles.Select(l => _mapper.Map<LibroGetDto>(l));                                                 
        }
    }
}
