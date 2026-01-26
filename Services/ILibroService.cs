using Thebook.DTOs;

namespace Thebook.Services
{
    public interface ILibroService
    {
        Task<IEnumerable<LibroGetDto>> Get();
        Task<LibroGetDto> GetByTitulo(string titulo);
    }
}
