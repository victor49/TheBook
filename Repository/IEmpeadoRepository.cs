using Thebook.Models;

namespace Thebook.Repository
{
    public interface IEmpeadoRepository
    {
        Task<Empleado> GetById(int id);
        Task Add(Empleado empleado);
        void Update(Empleado empleado);
        void Delete(Empleado empleado);
        Task Save();
    }
}
