using FluentValidation;
using Thebook.DTOs;

namespace Thebook.Validators
{
    public class UsuarioUpdateValidator : AbstractValidator<UsuarioUpdateDto>
    {
        public UsuarioUpdateValidator() 
        {
            RuleFor(u => u.Nombre).NotEmpty().WithMessage("El {PropertyName} es obligatorio")
                                  .MinimumLength(9).WithMessage("El {PropertyName} debe tener almenos 8 caracteres");

            RuleFor(u => u.Apellido).NotEmpty().WithMessage("El {PropertyName} es obligatorio")
                                  .MinimumLength(9).WithMessage("El {PropertyName} debe tener almenos 8 caracteres");

            RuleFor(u => u.Identificacion).NotEmpty().WithMessage("La {PropertyName} es obligatoria")
                                          .Matches(@"^\d+$").WithMessage("La {PropertyName} solo permite numeros");

            RuleFor(u => u.Correo).NotEmpty().WithMessage("El {PropertyName}")
                                  .EmailAddress().WithMessage("El {PopertyName} no cumple con el formato");

            RuleFor(u => u.Celular).NotEmpty().WithMessage("El {PropertyName} es obligatorio")
                                   .Matches(@"^\d+$").WithMessage("El {PropertyName} solo permite numeros")
                                   .MinimumLength(10).MaximumLength(10).WithMessage("El {PropertyName} debe tener 10 numeros");
        }
    }
}
