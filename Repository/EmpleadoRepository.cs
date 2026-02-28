using Microsoft.EntityFrameworkCore;
using Thebook.Models;
using Thebook.Repository;

namespace Thebook.Services
{
    public class EmpleadoRepository : IEmpleadoRepository
    {
        private readonly TheBookContext _context;
        public EmpleadoRepository(TheBookContext context)
        {
            _context = context;
        }
        public async Task<Empleado> GetById(int id)
            => await _context.Empleados.FindAsync(id);
        public async Task Add(Empleado empleado)
            => await _context.Empleados.AddAsync(empleado);

        public void Update(Empleado empleado)
        {
            _context.Empleados.Attach(empleado);
            _context.Empleados.Entry(empleado).State = EntityState.Modified;
        }

        public void Delete(Empleado empleado)
            => _context.Empleados.Remove(empleado);

        public async Task Save()
            => await _context.SaveChangesAsync();
            
    }
}
