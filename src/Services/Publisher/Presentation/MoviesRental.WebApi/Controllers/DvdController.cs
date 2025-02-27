using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoviesRental.Application.UseCases.Dvds.Commands.CreateDvd;
using MoviesRental.Application.UseCases.Dvds.Commands.RentDvd;
using MoviesRental.Application.UseCases.Dvds.Commands.ReturnDvd;
using MoviesRental.Application.UseCases.Dvds.Commands.UpdateDvd;
using MoviesRental.Core;
using MoviesRental.Core.EventBus.Events;
using MoviesRental.Query.Application.UseCases.Dvds.Queries.GetDvd;
using MoviesRental.WebApi.Cache;
using System.Net;

namespace MoviesRental.WebApi.Controllers;

public class DvdController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ICacheRepository _cacheRepository;

    public DvdController(
        IMediator mediator,
        IPublishEndpoint publisherEndpoint,
        ICacheRepository cacheRepository)
    {
        _mediator = mediator;
        _publishEndpoint = publisherEndpoint;
        _cacheRepository = cacheRepository;
    }

    [HttpGet("[action]/{title}", Name = "GetDvd")]
    [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult> GetDvd([FromRoute] string title)
    {
        var response = await _cacheRepository.Get(title);
        if (response is not null)
            return CustomResponse((int)HttpStatusCode.OK, true, response);

        var query = new GetDvdQuery(title);

        response = await _mediator.Send(query, HttpContext.RequestAborted);

        if (response is null)
            return CustomResponse((int)HttpStatusCode.NotFound, false);

        await _cacheRepository.Update(response);

        return CustomResponse((int)HttpStatusCode.OK, true, response);
    }

    [HttpPost("CreateDvd")]
    [ProducesResponseType(typeof(CreateDvdResponse), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult> CreateDvd(
       [FromBody] CreateDvdCommand request)
    {
        var response = await _mediator.Send(request, HttpContext.RequestAborted);

        if (response is null)
            return CustomResponse((int)HttpStatusCode.BadRequest, false);

        var @event = new DvdCreatedEvent(
            response.Id,
            response.Title,
            response.Genre,
            response.Published,
            response.Available,
            response.Copies,
            response.DirectorId,
            response.CreatedAt,
            response.UpdatedAt);

        await _publishEndpoint.Publish(@event);

        return CustomResponse((int)HttpStatusCode.Created, true, response);
    }

    [HttpPut("UpdateDvd")]
    [ProducesResponseType(typeof(CreateDvdResponse), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult> UpdateDvd(
        [FromBody] UpdateDvdCommand request)
    {
        var response = await _mediator.Send(request, HttpContext.RequestAborted);

        if (response is null)
            return CustomResponse((int)HttpStatusCode.BadRequest, false);

        var @event = new DvdUpdatedEvent(
            response.Id,
            response.Title,
            response.Genre,
            response.Published,
            response.Copies,
            response.DirectorId,
            response.UpdatedAt);

        await _publishEndpoint.Publish(@event);

        return CustomResponse((int)HttpStatusCode.OK, true, response);
    }

    [HttpPut("RentDvd")]
    [ProducesResponseType(typeof(CreateDvdResponse), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult> RentDvd(
        [FromBody] RentDvdCommand request)
    {
        var response = await _mediator.Send(request, HttpContext.RequestAborted);

        if (response is null)
            return CustomResponse((int)HttpStatusCode.BadRequest, false);

        var @event = new DvdRentedEvent(
            response.Id,
            response.UpdatedAt);

        await _publishEndpoint.Publish(@event);

        return CustomResponse((int)HttpStatusCode.OK, true, response);
    }

    [HttpPut("ReturnDvd")]
    [ProducesResponseType(typeof(CreateDvdResponse), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult> ReturnDvd(
    [FromBody] ReturnDvdCommand request)
    {
        var response = await _mediator.Send(request, HttpContext.RequestAborted);

        if (response is null)
            return CustomResponse((int)HttpStatusCode.BadRequest, false);

        var @event = new DvdReturnedEvent(
            response.Id,
            response.UpdatedAt);

        await _publishEndpoint.Publish(@event);

        return CustomResponse((int)HttpStatusCode.OK, true, response);
    }

    [HttpDelete("DeleteDvd")]
    [ProducesResponseType(typeof(CreateDvdResponse), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult> DeleteDvd(
        [FromBody] ReturnDvdCommand request)
    {
        var response = await _mediator.Send(request, HttpContext.RequestAborted);

        if (response is null)
            return CustomResponse((int)HttpStatusCode.BadRequest, false);

        var @event = new DvdDeletedEvent(
            response.Id,
            response.UpdatedAt);

        await _publishEndpoint.Publish(@event);

        return CustomResponse((int)HttpStatusCode.OK, true, response);
    }
}
