using Thebook.Models;

namespace Thebook.Repository
{
    public interface ILibroRepository
    {
        Task<IEnumerable<Libro>> Get();
        Task<Libro> GetByTitle(string titulo);
        Task Add(Libro libro);
        void Update(Libro libro);
        void Delete(Libro libro);
        Task Save();

        Task<bool> ExsiteLibro(int id);
    }
}
