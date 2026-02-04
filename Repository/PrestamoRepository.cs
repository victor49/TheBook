using Microsoft.EntityFrameworkCore;
using Thebook.Models;

namespace Thebook.Repository
{
    public class PrestamoRepository : IPrestamoRepository
    {
        public readonly TheBookContext _context;

        public PrestamoRepository(TheBookContext theContext)
        {
            _context = theContext;
        }

        public async Task Add(Prestamo prestamo)
            => await _context.Prestamos.AddAsync(prestamo);

        public async Task<int> CountPrestamosActivosByUsuario(int idUsuario)
        {
            return await _context.Prestamos
                .CountAsync(p =>
                    p.IdUsuario == idUsuario &&
                    p.FechaDevolucionReal == null
                );
        }

        public async Task Save()
            => await _context.SaveChangesAsync();
    }
}
