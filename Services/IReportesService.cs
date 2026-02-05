using Thebook.DTOs;

namespace Thebook.Services
{
    public interface IReportesService
    {
        Task<IEnumerable<PrestamoGetDto>> GetPrestamosActivos();
    }
}
