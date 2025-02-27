﻿using MediatR;
using MoviesRental.Application.Contracts;

namespace MoviesRental.Application.UseCases.Directors.Commands.UpdateDirector;
public class UpdateDirectorCommandHandler : IRequestHandler<UpdateDirectorCommand, UpdateDirectorResponse>
{
    private readonly IDirectorsWriteRepository _repository;
    private readonly UpdateDirectorCommandValidator _validator;

    public UpdateDirectorCommandHandler(IDirectorsWriteRepository repository, UpdateDirectorCommandValidator validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<UpdateDirectorResponse> Handle(UpdateDirectorCommand request, CancellationToken cancellationToken)
    {
        var validResult = _validator.Validate(request);

        if (!validResult.IsValid)
            return default;

        var director = await _repository.Get(request.Id);

        if (director is null)
            return default;

        director.UpdateName(request.Name);
        director.UpdateSurname(request.Surname);

        var result = await _repository.Update(director);

        if (!result)
            return default;

        return new UpdateDirectorResponse(director.Id.ToString(), director.Fullname(), director.UpdatedAt);
    }
}
