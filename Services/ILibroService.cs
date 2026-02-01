using Thebook.DTOs;

namespace Thebook.Services
{
    public interface ILibroService
    {
        Task<IEnumerable<LibroGetDto>> Get();
        Task<LibroGetDto> GetByTitulo(string titulo);
        Task<LibroGetDto> Add(LibroInsertDto libroInsertDto);
        Task<LibroGetDto> Update(string Titulo, LibroUpdateDto libroUpdateDto);
        Task<LibroGetDto> Delete(string titulo);

        Task UpdateCantidaLibro(int id);
    }
}
