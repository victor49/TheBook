using Thebook.Models;

namespace Thebook.Repository
{
    public interface IPrestamoRepository
    {
        Task Add(Prestamo prestamo);
        Task Save();
    }
}
