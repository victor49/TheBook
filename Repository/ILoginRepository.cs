using Thebook.Models;

namespace Thebook.Repository
{
    public interface ILoginRepository
    {
        Task<Empleado> GetEmpleado(string email);
        Task Save();
    }
}
