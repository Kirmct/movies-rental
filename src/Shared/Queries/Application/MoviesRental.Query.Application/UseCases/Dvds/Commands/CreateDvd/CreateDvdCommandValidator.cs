using FluentValidation;
using MoviesRental.Core.ValidationMessages;

namespace MoviesRental.Query.Application.UseCases.Dvds.Commands.CreateDvd;
public class CreateDvdCommandValidator : AbstractValidator<CreateDvdCommand>
{
    private const int COPIES_ERROR_NUMBER = -1;
    private const int MIN_TITLE_LENGTH = 3;
    private const int MAX_TITLE_LENGTH = 60;
    public CreateDvdCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage(ValidationMessages.EMPTY_STRING_ERROR_MESSAGE)
            .MinimumLength(MIN_TITLE_LENGTH).WithMessage(ValidationMessages.MIN_LENGTH_ERROR_MESSAGE)
            .MaximumLength(MAX_TITLE_LENGTH).WithMessage(ValidationMessages.MAX_LENGTH_ERROR_MESSAGE);

        RuleFor(x => x.Genre)
            .NotEmpty().WithMessage(ValidationMessages.ERROR_MESSAGE);

        RuleFor(x => x.Available)
            .Equal(true).WithMessage(ValidationMessages.ERROR_MESSAGE);

        RuleFor(x => x.Copies)
            .LessThan(COPIES_ERROR_NUMBER).WithMessage(ValidationMessages.ERROR_MESSAGE);

        RuleFor(x => x.Published)
            .LessThan(DateTime.Now).WithMessage(ValidationMessages.ERROR_MESSAGE);

        RuleFor(x => x.DirectorId)
            .NotEmpty().WithMessage(ValidationMessages.ERROR_MESSAGE);

        RuleFor(x => x.CreatedAt)
            .LessThan(DateTime.Now).WithMessage(ValidationMessages.ERROR_MESSAGE);

        RuleFor(x => x.UpdatedAt)
            .LessThan(DateTime.Now).WithMessage(ValidationMessages.ERROR_MESSAGE);
    }
}
