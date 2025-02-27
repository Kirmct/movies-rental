using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MoviesRental.Core;

namespace MoviesRental.WebApi.Controllers;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class ApiController : ControllerBase
{
    protected ActionResult CustomResponse(
        int status, bool success, object data = null)
    {
        return (status, success) switch
        {
            (404, false) => NotFound(new BaseResponse { StatusCode = status, Success = success, Message = "No Elements Found." }),
            (400, false) => BadRequest(new BaseResponse { StatusCode = status, Success = success, Message = "Erros during the transaction." }),
            (201, true) => Ok(new BaseResponse { StatusCode = status, Success = success, Message = "Created", Data = data }),
            (200, true) => Ok(new BaseResponse { StatusCode = status, Success = success, Data = data })
        };
    }
}
