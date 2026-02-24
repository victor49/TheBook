using FluentValidation;
using Thebook.DTOs;

namespace Thebook.Validators
{
    public class EmpleadoInsertValidator : AbstractValidator<EmpleadoInsertDto>
    {
        public EmpleadoInsertValidator() 
        {
            RuleFor(e => e.Email).NotEmpty().WithMessage("El Email es obligatorio")
                                 .EmailAddress().WithMessage("El formato del correo no es valido");
            RuleFor(e => e.Password).NotEmpty().WithMessage("La contraseña es obligatoria")
                                    .MinimumLength(8).WithMessage("Debe tener almemos 8 caracteres");
            RuleFor(e => e.Rol).NotEmpty().WithMessage("El ron es obligatorio");
        }
    }
}
