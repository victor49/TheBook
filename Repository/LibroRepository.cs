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

        public async Task<Libro> GetByTitle(string titulo)
            => await _context.Libros.AsNoTracking()
            .FirstOrDefaultAsync(l =>l.Titulo.ToLower() == titulo.ToLower().Trim());

        public Task Add(Libro libro)
        {
            throw new NotImplementedException();
        }

        public void Update(Libro libro)
        {
            throw new NotImplementedException();
        }

        public void Delete(Libro libro)
        {
            throw new NotImplementedException();
        }            

        public Task Save()
        {
            throw new NotImplementedException();
        }        
    }
}
