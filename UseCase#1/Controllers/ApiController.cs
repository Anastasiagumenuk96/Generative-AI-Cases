using Microsoft.AspNetCore.Mvc;
using MediatR;
using UseCase_1.Queries;
using UseCase_1.Models;

namespace UseCase_1.Controllers;

[ApiController]
[Route("[controller]")]
public class ApiController : ControllerBase
{
    private readonly IMediator _mediator;

    public ApiController(IMediator mediator) => _mediator = mediator;   
    
    [HttpPost("Countries")]
    public async Task<ActionResult<IReadOnlyCollection<Country>>> GetCountries(GetCountriesQuery query)
    {
        var request = await _mediator.Send(query);

        return Ok(request);
    }
}
