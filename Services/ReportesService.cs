using AutoMapper;
using Thebook.DTOs;
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

            return prestamosActivos.Select(p => _mapper.Map<PrestamoGetDto>(p)).ToList();

        }
        public async Task<IEnumerable<PrestamoGetDto>> GetPrestamosPorUsuario(int idUsuario)
        {
            var prestamos = await _prestamoRepository.GetPrestamosPorUsuario(idUsuario);

            return prestamos.Select(p => _mapper.Map<PrestamoGetDto>(p));
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
