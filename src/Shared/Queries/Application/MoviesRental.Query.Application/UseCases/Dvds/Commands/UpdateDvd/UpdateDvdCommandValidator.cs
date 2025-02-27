using FluentValidation;
using MoviesRental.Core.ValidationMessages;

namespace MoviesRental.Query.Application.UseCases.Dvds.Commands.UpdateDvd;
public class UpdateDvdCommandValidator : AbstractValidator<UpdateDvdCommand>
{
    public const int COPIES_ERROR_NUMBER = -1;
    public const int MIN_TITLE_LENGTH = 3;
    public const int MAX_TITLE_LENGTH = 60;
    public UpdateDvdCommandValidator()
    {
        RuleFor(x => x.Id)
           .NotEmpty().WithMessage(ValidationMessages.EMPTY_STRING_ERROR_MESSAGE);

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage(ValidationMessages.EMPTY_STRING_ERROR_MESSAGE)
            .MinimumLength(MIN_TITLE_LENGTH).WithMessage(ValidationMessages.MIN_LENGTH_ERROR_MESSAGE)
            .MaximumLength(MAX_TITLE_LENGTH).WithMessage(ValidationMessages.MAX_LENGTH_ERROR_MESSAGE);

        RuleFor(x => x.Genre)
            .NotEmpty().WithMessage(ValidationMessages.ERROR_MESSAGE);

        RuleFor(x => x.Published)
            .LessThan(DateTime.Now).WithMessage(ValidationMessages.ERROR_MESSAGE);

        RuleFor(x => x.Copies)
            .GreaterThan(COPIES_ERROR_NUMBER).WithMessage(ValidationMessages.ERROR_MESSAGE);

        RuleFor(x => x.DirectorId)
            .NotEmpty().WithMessage(ValidationMessages.EMPTY_STRING_ERROR_MESSAGE);

        RuleFor(x => x.UpdatedAt)
            .LessThan(DateTime.Now).WithMessage(ValidationMessages.ERROR_MESSAGE);
    }
}
