using AutoMapper;
using Thebook.DTOs;
using Thebook.Exceptions;
using Thebook.Models;
using Thebook.Repository;

namespace Thebook.Services
{
    public class EmpleadoService: IEmpleadoService
    {
        private readonly IEmpleadoRepository _empeadoRepository;
        private readonly IMapper _mapper;

        private static string nombreModelo = "Empleado";

        public EmpleadoService(IEmpleadoRepository empeadoRepository, IMapper mapper)
        {
            _empeadoRepository = empeadoRepository;
            _mapper = mapper;
        }

        public async Task<EmpleadoGetDto> GetById(int id)
        {
            var empleado = await _empeadoRepository.GetById(id);

            if (empleado == null)
                throw new NotFoundException(nombreModelo, id);
            
            else       
            {
                var empleadoDto = _mapper.Map<EmpleadoGetDto>(empleado);
                return empleadoDto;
            }
        }

        public async Task<EmpleadoGetDto> Add(EmpleadoInsertDto empleadoInsertDto)
        {
            var empleado = _mapper.Map<Empleado>(empleadoInsertDto);
            await _empeadoRepository.Add(empleado);
            await _empeadoRepository.Save();

            var empleadoDto = _mapper.Map<EmpleadoGetDto>(empleado);
            return empleadoDto;
        }

        public async Task<EmpleadoGetDto> Update(int id, EmpleadoUpdateDto empleadoUpdateDto)
        {
            var empleado = await _empeadoRepository.GetById(id);
            
            if (empleado == null)
                throw new NotFoundException(nombreModelo, id);

            else
            {
                empleado = _mapper.Map(empleadoUpdateDto, empleado);

                _empeadoRepository.Update(empleado);
                await _empeadoRepository.Save();

                var empleadoDto = _mapper.Map<EmpleadoGetDto>(empleado);
                return empleadoDto;
            }      
        }

        public async Task<EmpleadoGetDto> Delete(int id)
        {
            var empleado = await _empeadoRepository.GetById(id);

            if (empleado == null)
                throw new NotFoundException(nombreModelo, id);

            else
            {
                var empleadoDto = _mapper.Map<EmpleadoGetDto>(empleado);
                _empeadoRepository.Delete(empleado);
                await _empeadoRepository.Save();

                return empleadoDto;
            }
        }                
    }
}
