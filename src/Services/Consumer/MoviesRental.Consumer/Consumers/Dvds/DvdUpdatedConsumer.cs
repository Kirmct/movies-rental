using MassTransit;
using MediatR;
using MoviesRental.Core.EventBus.Events;
using MoviesRental.Query.Application.UseCases.Dvds.Commands.UpdateDvd;

namespace MoviesRental.Consumer.Consumers.Dvds;

public class DvdUpdatedConsumer : IConsumer<DvdUpdatedEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;

    public DvdUpdatedConsumer(IMediator mediator, ILogger logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<DvdUpdatedEvent> context)
    {
        try
        {
            var @event = context?.Message ?? throw new ArgumentNullException(nameof(context));

            var command = new UpdateDvdCommand(
                @event.Id,
                @event.Title,
                @event.Genre,
                @event.Published,
                @event.Copies,
                @event.DirectorId,
                @event.UpdatedAt);

            _logger.LogInformation($"Creating dvd {command.Title}");

            var response = await _mediator.Send(command, default);

            if (!response)
            {
                _logger.LogInformation($"Something wrong happened during the proccess of update dvd {@event.Id}");
                throw new InvalidOperationException($"Failed to update Dvd {@event.Id}");
            }

            _logger.LogInformation($"Dvd {@event.Id} update");
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex, "An error occurred while consuming the DvdUpdatedEvent");
            throw;
        }
    }
}
