using MassTransit;
using MediatR;
using MoviesRental.Core.EventBus.Events;
using MoviesRental.Query.Application.UseCases.Dvds.Commands.ReturnDvd;

namespace MoviesRental.Consumer.Consumers.Dvds;

public class DvdReturnedConsumer : IConsumer<DvdReturnedEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;

    public DvdReturnedConsumer(IMediator mediator, ILogger logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<DvdReturnedEvent> context)
    {
        try
        {
            var @event = context?.Message ?? throw new ArgumentNullException(nameof(context));

            var command = new ReturnDvdCommand(@event.Id, @event.UpdatedAt);

            _logger.LogInformation($"Returning dvd {command.Id}");

            var response = await _mediator.Send(command, default);

            if (!response)
            {
                _logger.LogInformation($"Something wrong happened during the proccess of return dvd {@event.Id}");
                throw new InvalidOperationException($"Failed to return dvd {@event.Id}");
            }

            _logger.LogInformation($"Dvd {@event.Id} return");
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex, "An error occurred while consuming the DvdReturnedEvent");
            throw;
        }
    }
}
