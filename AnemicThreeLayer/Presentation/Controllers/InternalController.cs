using Application.Dto;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class InternalController : ControllerBase
{
    private readonly IAccountService _accountService;

    public InternalController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("create-admin")]
    public async Task<IActionResult> CreateAdministratorAccount([FromQuery] string name, [FromQuery] string password)
    {
        AccountDto account = await _accountService.CreateAccount(name, password);
        return this.Ok(account);
    }

    [HttpPost("grant-groups-role")]
    public async Task<IActionResult> GrantGroupsRole([FromQuery] Guid accountId)
    {
        AccountDto account = await _accountService.GrantGroupCreation(accountId);
        return this.Ok(account);
    }

    [HttpPost("grant-student-role")]
    public async Task<IActionResult> GrantStudentRole([FromQuery] Guid accountId)
    {
        AccountDto account = await _accountService.GrantStudentCreation(accountId);
        return this.Ok(account);
    }
}