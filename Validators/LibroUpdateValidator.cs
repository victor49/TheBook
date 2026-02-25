using FluentValidation;
using Thebook.DTOs;

namespace Thebook.Validators
{
    public class LibroUpdateValidator : AbstractValidator<LibroUpdateDto>
    {
        public LibroUpdateValidator() 
        {
            RuleFor(l => l.Titulo).NotEmpty().WithMessage("El {PropertyName} es obligatorio")
                                  .MinimumLength(5).WithMessage("El {PropertyName} debe tener almenos 5 catacteres");
            RuleFor(l => l.Autor).NotEmpty().WithMessage("El {PropertyName} es obligatorio");
            RuleFor(l => l.Editorial).NotEmpty().WithMessage("El {PropertyName} es obligatorio");
            RuleFor(l => l.Categoria).NotEmpty().WithMessage("El {PropertyName} es obligatorio");
            RuleFor(l => l.CantidadDisponible).NotNull().WithMessage("El {PropertyName} es obligatorio")
                                              .GreaterThanOrEqualTo(0).WithMessage("La {PropertyName} no puede ser negativa");
        }
    }
}
