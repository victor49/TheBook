using FluentValidation;
using Thebook.DTOs;

namespace Thebook.Validators
{
    public class PrestamoInsertValidator : AbstractValidator<PrestamoInsertDto>
    {
        public PrestamoInsertValidator()
        {
            RuleFor(p => p.FechaDevolucionEstimada).NotEmpty().WithMessage("La {PropertyName} es obligatoria");

            RuleFor(p => p.IdUsuario).NotEmpty().WithMessage("El {PropertyName} es obligatorio");
            RuleFor(p => p.IdLibro).NotEmpty().WithMessage("El {PropertyName} es obligatorio");
        }
    }
}
