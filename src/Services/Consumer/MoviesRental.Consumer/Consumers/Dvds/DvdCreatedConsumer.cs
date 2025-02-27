using MassTransit;
using MediatR;
using MoviesRental.Core.EventBus.Events;
using MoviesRental.Query.Application.UseCases.Dvds.Commands.CreateDvd;

namespace MoviesRental.Consumer.Consumers.Dvds;

public class DvdCreatedConsumer : IConsumer<DvdCreatedEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;

    public DvdCreatedConsumer(IMediator mediator, ILogger logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<DvdCreatedEvent> context)
    {
        try
        {
            var @event = context?.Message ?? throw new ArgumentNullException(nameof(context));

            var command = new CreateDvdCommand(
                @event.Id, 
                @event.Title, 
                @event.Genre, 
                @event.Published,
                @event.Available,
                @event.Copies,
                @event.DirectorId,
                @event.CreatedAt,
                @event.UpdatedAt);

            _logger.LogInformation($"Creating dvd {command.Title}");

            var response = await _mediator.Send(command, default);

            if (!response)
            {
                _logger.LogInformation($"Something wrong happened during the proccess of create dvd {@event.Id}");
                throw new InvalidOperationException($"Failed to create Dvd {@event.Id}");
            }

            _logger.LogInformation($"Dvd {@event.Id} created");
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex, "An error occurred while consuming the DvdCreatedEvent");
            throw;
        }
    }
}
