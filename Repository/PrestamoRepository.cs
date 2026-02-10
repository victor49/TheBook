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

        public async Task<IEnumerable<Prestamo>> GetPrestamosActivos()
        {
            return await _context.Prestamos.Include(p => p.Usuarios)
                                           .Include(p => p.Libros)
                                           .Where(p => p.FechaDevolucionReal == null)
                                           .ToListAsync();
        }

        public async Task<IEnumerable<Prestamo>> GetPrestamosPorUsuario(int id)
        {
            return await _context.Prestamos.Where(p => p.IdUsuario == id).ToListAsync();
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

        public async Task<int> LibroMasPrestado()
        {
            return await _context.Prestamos.GroupBy(p => p.IdLibro)
                                            .OrderByDescending(g => g.Count())
                                            .Select(g => g.Key)
                                            .FirstOrDefaultAsync();
        }

        public async Task Save()
            => await _context.SaveChangesAsync();

        
    }
}
