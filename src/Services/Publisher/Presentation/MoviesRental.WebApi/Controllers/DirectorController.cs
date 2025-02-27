using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoviesRental.Application.UseCases.Directors.Commands.CreateDirector;
using MoviesRental.Application.UseCases.Directors.Commands.DeleteDirector;
using MoviesRental.Application.UseCases.Directors.Commands.UpdateDirector;
using MoviesRental.Core;
using MoviesRental.Core.EventBus.Events;
using MoviesRental.Query.Application.UseCases.Directors.Queries.GetDirector;
using System.Net;

namespace MoviesRental.WebApi.Controllers;

public class DirectorController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IPublishEndpoint _publishEndpoint;

    public DirectorController(
        IMediator mediator,
        IPublishEndpoint publishEndpoint)
    {
        _mediator = mediator;
        _publishEndpoint = publishEndpoint;
    }

    [HttpGet("[action]/{fullname}", Name = "GetDirector")]
    [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult> GetDirector([FromRoute] string fullname)
    {
        var query = new GetDirectorQuery(fullname);

        var response = await _mediator.Send(query, HttpContext.RequestAborted);

        if (response is null)
            return CustomResponse((int)HttpStatusCode.NotFound, false);

        return CustomResponse((int)HttpStatusCode.OK, true, response);
    }

    [HttpPost("CreateDirector")]
    [ProducesResponseType(typeof(CreateDirectorResponse), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult> CreateDirector(
        [FromBody] CreateDirectorCommand request)
    {
        var response = await _mediator.Send(request, HttpContext.RequestAborted);

        if (response is null)
            return CustomResponse((int)HttpStatusCode.BadRequest, false);

        var @event = new DirectorCreatedEvent(
            response.Id,
            response.FullName,
            response.CreatedAt,
            response.UpdatedAt);

        await _publishEndpoint.Publish(@event);

        return CustomResponse((int)HttpStatusCode.Created, true, response);
    }

    [HttpPut("UpdateDirector")]
    [ProducesResponseType(typeof(CreateDirectorResponse), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult> UpdateDirector(
        [FromBody] UpdateDirectorCommand request)
    {
        var response = await _mediator.Send(request, HttpContext.RequestAborted);

        if (response is null)
            return CustomResponse((int)HttpStatusCode.BadRequest, false);

        var @event = new DirectorUpdatedEvent(
           response.Id,
           response.FullName,
           response.UpdatedAt);

        await _publishEndpoint.Publish(@event);

        return CustomResponse((int)HttpStatusCode.OK, true, response);
    }

    [HttpDelete("DeleteDirector")]
    [ProducesResponseType(typeof(CreateDirectorResponse), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult> DeleteDirector(
       [FromBody] DeleteDirectorCommand request)
    {
        var response = await _mediator.Send(request, HttpContext.RequestAborted);

        if (!response)
            return CustomResponse((int)HttpStatusCode.BadRequest, false);

        var @event = new DirectorDeletedEvent(request.Id.ToString());

        await _publishEndpoint.Publish(@event);

        return CustomResponse((int)HttpStatusCode.OK, true, response);
    }

}
