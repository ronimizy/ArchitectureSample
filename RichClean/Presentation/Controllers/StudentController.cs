using Application.Contracts.Students;
using Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Students;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly IMediator _mediator;

    public StudentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    public async Task<ActionResult<StudentDto>> CreateAsync([FromBody] CreateStudentModel model)
    {
        var command = new CreateStudent.Command(model.Name, model.GroupId);
        var response = await _mediator.Send(command, CancellationToken);

        return Ok(response.Student);
    }
}