using AutoMapper;
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
        private readonly IMapper _mapper;

        public DevolucionService(IDevolucionRepository devolucionRepository, ILibroService libroService,
                                 IMapper mapper)
        {
            _devolucionRepository = devolucionRepository;
            _libroService = libroService;
            _mapper = mapper;
        }

        public async Task<ServiceResult<PrestamoGetDto>> UpdateDevolucion(int id)
        {
            var prestamo = await _devolucionRepository.GetById(id);

            if (prestamo == null)
                throw new BusinessException("Prestamo no existe");            

            else
            {
                //Agregar la fecha de la devolucion del libro
                prestamo.FechaDevolucionReal = DateOnly.FromDateTime(DateTime.Now);

                if (prestamo.FechaDevolucionReal < prestamo.FechaPrestamo)
                    throw new BusinessException("La fecha de devolución no puede ser anterior a la fecha de préstamo.");

                else
                {
                    _devolucionRepository.Update(prestamo);
                    await _devolucionRepository.Save();

                    await _libroService.UpdateCantidaLibroAumentar(prestamo.IdLibro);

                    var prestamoDto = _mapper.Map<PrestamoGetDto>(prestamo);

                    // Si la devolución se realizó fuera de la fecha estimada. Con un Generic
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
