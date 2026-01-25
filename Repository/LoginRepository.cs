using Microsoft.EntityFrameworkCore;
using Thebook.Models;

namespace Thebook.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly TheBookContext _context;

        public LoginRepository(TheBookContext context)
        {
            _context = context;
        }

        public async Task<Empleado> GetEmpleado(string email)
           => await _context.Empleados.FirstOrDefaultAsync(e => e.Email == email);

        public Task Save()
        {
            throw new NotImplementedException();
        }
    }
}
