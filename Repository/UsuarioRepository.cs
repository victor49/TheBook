using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Thebook.Models;

namespace Thebook.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly TheBookContext _context;

        public UsuarioRepository(TheBookContext context)
        {
            _context = context; 
        }

        public async Task<IEnumerable<Usuario>> Get()
            => await _context.Usuarios.ToListAsync();

        public async Task<Usuario> GetById(int id)
            => await _context.Usuarios.FindAsync(id);
        
        public async Task Add(Usuario usuario)
            => await _context.Usuarios.AddAsync(usuario);        

        public void Update(Usuario usuario)
        {
            _context.Usuarios.Attach(usuario);
            _context.Usuarios.Entry(usuario).State = EntityState.Modified;
        }

        public async Task Save()
            => await _context.SaveChangesAsync();        

        public async Task<bool> ExisteUsuario(int id)
        {
            return await _context.Usuarios.AnyAsync(u => u.IdUsuario == id);
        }
    }
}
