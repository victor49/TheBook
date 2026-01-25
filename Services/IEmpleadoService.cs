using Thebook.DTOs;

namespace Thebook.Services
{
    public interface IEmpleadoService
    {
        Task<EmpleadoGetDto> GetById(int id);
        Task<EmpleadoGetDto> Add(EmpleadoInsertDto empleadoInsertDto);
        Task<EmpleadoGetDto> Update(int id, EmpleadoUpdateDto empleadoUpdateDto);
        Task<EmpleadoGetDto> Delete(int id);
    }
}
