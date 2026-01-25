using Thebook.DTOs;
using Thebook.Models;
using Thebook.Repository;

namespace Thebook.Services
{
    public class EmpleadoService: IEmpleadoService
    {
        private readonly IEmpeadoRepository _empeadoRepository;

        public EmpleadoService(IEmpeadoRepository empeadoRepository)
        {
            _empeadoRepository = empeadoRepository;
        }

        public async Task<EmpleadoGetDto> GetById(int id)
        {
            var empleado = await _empeadoRepository.GetById(id);
            if (empleado != null)
            {
                var empleadoDto = new EmpleadoGetDto
                {
                    IdEmpleado = empleado.IdEmpleado,
                    Email = empleado.Email,                    
                    Rol = empleado.Rol
                };
                return empleadoDto;
            }
            return null;
        }

        public async Task<EmpleadoGetDto> Add(EmpleadoInsertDto empleadoInsertDto)
        {
            var empleado = new Empleado()
            {
                Email = empleadoInsertDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(empleadoInsertDto.Password),
                Rol = empleadoInsertDto.Rol
            };
            await _empeadoRepository.Add(empleado);
            await _empeadoRepository.Save();

            var empleadoDto = new EmpleadoGetDto
            {
                IdEmpleado = empleado.IdEmpleado,
                Email = empleado.Email,
                Rol = empleado.Rol
            };
            return empleadoDto;
        }

        public async Task<EmpleadoGetDto> Update(int id, EmpleadoUpdateDto empleadoUpdateDto)
        {
            var empleado = await _empeadoRepository.GetById(id);

            if(empleado != null)
            {
                empleado.Email = empleadoUpdateDto.Email;
                empleado.Password = BCrypt.Net.BCrypt.HashPassword(empleadoUpdateDto.Password);
                empleado.Rol = empleadoUpdateDto.Rol;

                _empeadoRepository.Update(empleado);
                await _empeadoRepository.Save();

                var empleadoDto = new EmpleadoGetDto
                {
                    IdEmpleado = empleado.IdEmpleado,
                    Email = empleado.Email,
                    Rol = empleado.Rol
                };
                return empleadoDto;
            }
            return null;
        }

        public async Task<EmpleadoGetDto> Delete(int id)
        {
            var empleado = await _empeadoRepository.GetById(id);

            if (empleado != null)
            {
                var empleadoDto = new EmpleadoGetDto
                {
                    IdEmpleado = empleado.IdEmpleado,
                    Email = empleado.Email,
                    Rol = empleado.Rol
                };
                _empeadoRepository.Delete(empleado);
                await _empeadoRepository.Save();

                return empleadoDto;
            }
            return null;
        }                
    }
}
