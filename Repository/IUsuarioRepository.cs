using Microsoft.AspNetCore.Mvc;
using Thebook.Models;

namespace Thebook.Repository
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> Get();
        Task<Usuario> GetById(int id);
        Task Add(Usuario usuario);
        void Update(Usuario usuario);
        Task Save();       
    }
}
