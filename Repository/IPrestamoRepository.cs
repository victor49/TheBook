using Thebook.Models;

namespace Thebook.Repository
{
    public interface IPrestamoRepository
    {
        Task<IEnumerable<Prestamo>> GetPrestamosActivos();
        Task<IEnumerable<Prestamo>> GetPrestamosPorUsuario(int id);
        Task Add(Prestamo prestamo);     
        Task<int> CountPrestamosActivosByUsuario(int idUsuario);
        Task<int> LibroMasPrestado();

        Task Save();
    }
}
