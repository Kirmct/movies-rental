using MassTransit;
using MediatR;
using MoviesRental.Core.EventBus.Events;
using MoviesRental.Query.Application.UseCases.Dvds.Commands.DeleteDvd;

namespace MoviesRental.Consumer.Consumers.Dvds;

public class DvdDeletedConsumer : IConsumer<DvdDeletedEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;

    public DvdDeletedConsumer(IMediator mediator, ILogger logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<DvdDeletedEvent> context)
    {
        try
        {
            var @event = context?.Message ?? throw new ArgumentNullException(nameof(context));

            var command = new DeleteDvdCommand(@event.Id, @event.DeletedAt);

            _logger.LogInformation($"Creating director {command.Id}");

            var response = await _mediator.Send(command, default);

            if (!response)
            {
                _logger.LogInformation($"Something wrong happened during the proccess of remove dvd {@event.Id}");
                throw new InvalidOperationException($"Failed to remove dvd {@event.Id}");
            }

            _logger.LogInformation($"Dvd {@event.Id} created");
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex, "An error occurred while consuming the DvdDeletedEvent");
            throw;
        }
    }
}
