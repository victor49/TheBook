using Thebook.DTOs;
using Thebook.Exceptions;
using Thebook.Repository;
using Thebook.Results;

namespace Thebook.Services
{
    public class DevolucionService : IDevolucionService
    {
        private readonly IDevolucionRepository _devolucionRepository;
        private readonly ILibroService _libroService;

        public DevolucionService(IDevolucionRepository devolucionRepository, ILibroService libroService)
        {
            _devolucionRepository = devolucionRepository;
            _libroService = libroService;
        }

        public async Task<ServiceResult<PrestamoGetDto>> UpdateDevolucion(int id)
        {
            var prestamo = await _devolucionRepository.GetById(id);

            if (prestamo == null)
                throw new BusinessException("Prestamo no existe");            

            else
            {
                prestamo.FechaDevolucionReal = DateOnly.FromDateTime(DateTime.Now);

                if (prestamo.FechaDevolucionReal < prestamo.FechaPrestamo)
                    throw new BusinessException("La fecha de devolución no puede ser anterior a la fecha de préstamo.");

                else
                {
                    _devolucionRepository.Update(prestamo);
                    await _devolucionRepository.Save();

                    await _libroService.UpdateCantidaLibroAumentar(prestamo.IdLibro);

                    var prestamoDto = new PrestamoGetDto
                    {
                        IdPrestamo = prestamo.IdPrestamo,
                        FechaPrestamo = prestamo.FechaPrestamo,
                        FechaDevolucionEstimada = prestamo.FechaDevolucionEstimada,
                        FechaDevolucionReal = DateOnly.FromDateTime(DateTime.Now),

                        Usuario = new UsuarioGetDto
                        {
                            IdUsuario = prestamo.Usuarios.IdUsuario,
                            Nombre = prestamo.Usuarios.Nombre,
                            Correo = prestamo.Usuarios.Correo
                        },

                        Libro = new LibroGetDto
                        {
                            IdLibro = prestamo.Libros.IdLibro,
                            Titulo = prestamo.Libros.Titulo,
                            Autor = prestamo.Libros.Autor,
                            CantidadDisponible = prestamo.Libros.CantidadDisponible
                        }
                    };

                    var result = new ServiceResult<PrestamoGetDto>
                    {
                        Success = true,
                        Data = prestamoDto
                    };

                    if (prestamo.FechaDevolucionReal > prestamo.FechaDevolucionEstimada)
                        result.Message = "la devolución se realizo fuera de la fecha estimada.";

                    return result;                                        
                }                              
            }
        }
    }
}
