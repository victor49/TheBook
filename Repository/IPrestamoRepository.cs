using Thebook.Models;

namespace Thebook.Repository
{
    public interface IPrestamoRepository
    {
        Task<IEnumerable<Prestamo>> GetPrestamosActivos();
        Task Add(Prestamo prestamo);     
        Task<int> CountPrestamosActivosByUsuario(int idUsuario);
        Task Save();
    }
}
