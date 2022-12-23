using System.Net;
using System.Security.Claims;
using Application.Dto;
using Application.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AuthenticationController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(string username, string password)
    {
        AccountDto account = await _accountService.FindAccount(username, password);

        if (account is null)
        {
            return AccessDenied();
        }

        var claims = new Claim[]
        {
            new Claim(ClaimTypes.Name, account.Name),
            new Claim(ClaimTypes.Sid, account.Id.ToString()),
            new Claim(ClaimTypes.Role, account.Role),
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

        return Ok(account);
    }

    [Route("error")]
    [HttpGet]
    [HttpPost]
    public IActionResult AccessDenied()
    {
        Claim roleClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);
        return roleClaim is not null
            ? this.StatusCode((int)HttpStatusCode.Forbidden, $"User with role {roleClaim.Value} is not authorized to invoke this method")
            : this.Unauthorized("User is not authenticated");
    }
}
