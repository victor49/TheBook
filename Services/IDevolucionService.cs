using Thebook.DTOs;

namespace Thebook.Services
{
    public interface IDevolucionService
    {
        Task<PrestamoGetDto> UpdateDevolucion(int id);
    }
}
