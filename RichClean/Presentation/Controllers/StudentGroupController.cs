using Application.Contracts.StudentGroups;
using Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.StudentGroups;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentGroupController : ControllerBase
{
    private readonly IMediator _mediator;

    public StudentGroupController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    public async Task<ActionResult<StudentGroupDto>> CreateAsync([FromBody] CreateStudentGroupModel model)
    {
        var command = new CreateStudentGroup.Command(model.Name, model.MaxStudentCount);
        var response = await _mediator.Send(command, CancellationToken);

        return Ok(response.Group);
    }
}