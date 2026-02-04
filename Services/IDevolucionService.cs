using Thebook.DTOs;
using Thebook.Results;

namespace Thebook.Services
{
    public interface IDevolucionService
    {
        Task<ServiceResult<PrestamoGetDto>> UpdateDevolucion(int id);
    }
}
