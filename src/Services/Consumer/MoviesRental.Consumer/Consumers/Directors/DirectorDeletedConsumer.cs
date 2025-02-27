using MassTransit;
using MediatR;
using MoviesRental.Core.EventBus.Events;
using MoviesRental.Query.Application.UseCases.Directors.Commands.DeleteDirector;

namespace MoviesRental.Consumer.Consumers.Directors;

public class DirectorDeletedConsumer : IConsumer<DirectorDeletedEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;

    public DirectorDeletedConsumer(IMediator mediator, ILogger logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<DirectorDeletedEvent> context)
    {
        try
        {
            var @event = context?.Message ?? throw new ArgumentNullException(nameof(context));
            var command = new DeleteDirectorCommand(@event.Id);
            _logger.LogInformation($"Removing director {command.Id}");

            var response = await _mediator.Send(command, default);

            if (!response)
            {
                _logger.LogInformation($"Something wrong happened during the proccess of remove director {@event.Id}");
                throw new InvalidOperationException($"Failed to create director {@event.Id}");
            }

            _logger.LogInformation($"Director {@event.Id} removed");
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex, "An error occurred while consuming the DirectorDeletedEvent");
            throw;
        }
    }
}
