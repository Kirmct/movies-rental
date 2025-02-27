using MassTransit;
using MediatR;
using MoviesRental.Core.EventBus.Events;
using MoviesRental.Query.Application.UseCases.Dvds.Commands.RentDvd;

namespace MoviesRental.Consumer.Consumers.Dvds;

public class DvdRentedConsumer : IConsumer<DvdRentedEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;

    public DvdRentedConsumer(IMediator mediator, ILogger logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<DvdRentedEvent> context)
    {
        try
        {
            var @event = context?.Message ?? throw new ArgumentNullException(nameof(context));

            var command = new RentDvdCommand(@event.Id, @event.UpdatedAt);

            _logger.LogInformation($"Renting dvd {command.Id}");

            var response = await _mediator.Send(command, default);

            if (!response)
            {
                _logger.LogInformation($"Something wrong happened during the proccess of rent dvd {@event.Id}");
                throw new InvalidOperationException($"Failed to rent dvd {@event.Id}");
            }

            _logger.LogInformation($"Dvd {@event.Id} rented");
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex, "An error occurred while consuming the DvdRentedEvent");
            throw;
        }
    }
}
