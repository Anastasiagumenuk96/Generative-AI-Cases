using Microsoft.AspNetCore.Mvc;
using MediatR;
using UseCase_1.Queries;
namespace UseCase_1.Controllers;

[ApiController]
[Route("[controller]")]
public class ApiController : ControllerBase
{
    private readonly IMediator _mediator;

    public ApiController(IMediator mediator) => _mediator = mediator;   
    
    [HttpGet("Countries")]
    public async Task<ActionResult> GetCountries()
    {
        var request = await _mediator.Send(new GetCountriesQuery());

        return Ok(request);
    }
}
