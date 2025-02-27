using MassTransit;
using MediatR;
using MoviesRental.Core.EventBus.Events;
using MoviesRental.Query.Application.UseCases.Directors.Commands.CreateDirector;

namespace MoviesRental.Consumer.Consumers.Directors;

public class DirectorCreatedConsumer : IConsumer<DirectorCreatedEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;

    public DirectorCreatedConsumer(IMediator mediator, ILogger logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<DirectorCreatedEvent> context)
    {
        try
        {
            var @event = context?.Message ?? throw new ArgumentNullException(nameof(context));

            var command = new CreateDirectorCommand(@event.Id, @event.FullName, @event.CreatedAt, @event.UpdatedAt);
            _logger.LogInformation($"Creating director {command.FullName}");

            var response = await _mediator.Send(command, default);

            if (!response)
            {
                _logger.LogInformation($"Something wrong happened during the proccess of create director {@event.Id}");
                throw new InvalidOperationException($"Failed to create director {@event.Id}");
            }

            _logger.LogInformation($"Director {@event.Id} created");
        }
        catch (Exception ex) 
        {
            _logger.LogInformation(ex, "An error occurred while consuming the DirectorCreatedEvent");
            throw;
        }
    }
}
