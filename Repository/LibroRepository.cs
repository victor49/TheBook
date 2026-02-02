using Microsoft.EntityFrameworkCore;
using Thebook.Models;

namespace Thebook.Repository
{
    public class LibroRepository : ILibroRepository
    {
        private readonly TheBookContext _context;

        public LibroRepository(TheBookContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Libro>> Get()
            => await _context.Libros.ToListAsync();

        public async Task<Libro> GetById(int id)
            => await _context.Libros.FindAsync(id);      

        public async Task<Libro> GetByTitle(string titulo)
            => await _context.Libros.AsNoTracking()
            .FirstOrDefaultAsync(l =>l.Titulo.ToLower() == titulo.ToLower().Trim());

        public async Task Add(Libro libro)
            => await _context.AddAsync(libro);

        public void Update(Libro libro)
        {
            _context.Libros.Attach(libro);
            _context.Libros.Entry(libro).State = EntityState.Modified;
        }

        public void Delete(Libro libro)
            => _context.Libros.Remove(libro);

        public async Task Save()
            => await _context.SaveChangesAsync();       
    }       
}
