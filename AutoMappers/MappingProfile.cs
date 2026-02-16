using AutoMapper;
using Thebook.DTOs;
using Thebook.Models;

namespace Thebook.AutoMappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //  Usuarios
            //Get
            CreateMap<Usuario, UsuarioGetDto>();
            //Add
            CreateMap<UsuarioInsertDto, Usuario>();
            //Update
            CreateMap<UsuarioUpdateDto, Usuario>();

            //  Libros
            //Get
            CreateMap<Libro, LibroGetDto>();
            //Add
            CreateMap<LibroInsertDto, Libro>();
            //Update
            CreateMap<LibroUpdateDto, Libro>();

            //  Empleados
            //Get
            CreateMap<Empleado, EmpleadoGetDto>();
            //Add
            CreateMap<EmpleadoInsertDto, Empleado>();
            //Update
            CreateMap<EmpleadoUpdateDto, Empleado>();

            //  Reportes y Prestamos
            //Prestamos Activos
            CreateMap<Prestamo, PrestamoGetDto>()
                .ForMember(dto => dto.Usuario,
                           m => m.MapFrom(u => u.Usuarios))
                .ForMember(dto => dto.Libro,
                           m => m.MapFrom(l => l.Libros));

            //  Prestamos
            //Add
            CreateMap<PrestamoInsertDto, Prestamo>();
        }
    }
}
