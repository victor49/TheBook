using Thebook.DTOs;

namespace Thebook.Services
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioGetDto>> Get();
        Task<UsuarioGetDto> GetById(int id);
        Task<UsuarioGetDto> Add(UsuarioInsertDto usuarioInsertDto);
        Task<UsuarioGetDto> Update(int id, UsuarioUpdateDto usuarioUpdateDto);
    }
}
