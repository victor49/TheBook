using Microsoft.EntityFrameworkCore;
using Thebook.Models;

namespace Thebook.Repository
{
    public class DevolucionRepository : IDevolucionRepository
    {
        private readonly TheBookContext _context;

        public DevolucionRepository(TheBookContext context)
        {
            _context = context;
        }

        public async Task<Prestamo> GetById(int id)
            => await _context.Prestamos.FindAsync(id);

        public void Update(Prestamo prestamo)
        {
            _context.Prestamos.Attach(prestamo);
            _context.Prestamos.Entry(prestamo).State = EntityState.Modified;                
        }

        public async Task Save()
            => await _context.SaveChangesAsync();
    }
}
