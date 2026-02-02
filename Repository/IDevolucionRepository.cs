using Thebook.Models;

namespace Thebook.Repository
{
    public interface IDevolucionRepository
    {
        Task<Prestamo> GetById(int id);
        void Update(Prestamo prestamo);

        Task Save();
    }
}
