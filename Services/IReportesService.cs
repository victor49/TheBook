using Thebook.DTOs;

namespace Thebook.Services
{
    public interface IReportesService
    {
        Task<IEnumerable<PrestamoGetDto>> GetPrestamosActivos();
        Task<IEnumerable<PrestamoGetDto>> GetPrestamosPorUsuario(int idUsuario);
    }
}
