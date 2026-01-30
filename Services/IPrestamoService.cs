using Thebook.DTOs;

namespace Thebook.Services
{
    public interface IPrestamoService
    {
        Task<PrestamoGetDto> Add(PrestamoInsertDto prestamoInsertDto);
    }
}
