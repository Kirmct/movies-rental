using MassTransit;
using MediatR;
using MoviesRental.Core.EventBus.Events;
using MoviesRental.Query.Application.UseCases.Directors.Commands.UpdateDirector;

namespace MoviesRental.Consumer.Consumers.Directors;

public class DirectorUpdatedConsumer : IConsumer<DirectorUpdatedEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;

    public DirectorUpdatedConsumer(IMediator mediator, ILogger logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<DirectorUpdatedEvent> context)
    {
        try
        {
            var @event = context?.Message ?? throw new ArgumentNullException(nameof(context));

            var command = new UpdateDirectorCommand(@event.Id, @event.FullName,@event.UpdatedAt);
            _logger.LogInformation($"Updating director {command.FullName}");

            var response = await _mediator.Send(command, default);

            if (!response)
            {
                _logger.LogInformation($"Something wrong happened during the proccess of update director {@event.Id}");
                throw new InvalidOperationException($"Failed to update director {@event.Id}");
            }

            _logger.LogInformation($"Director {@event.Id} created");
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex, "An error occurred while consuming the DirectorUpdatedEvent");
            throw;
        }
    }
}
