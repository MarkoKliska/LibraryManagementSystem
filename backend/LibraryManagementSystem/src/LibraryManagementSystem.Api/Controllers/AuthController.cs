using LibraryManagementSystem.Application.Authentication;
using LibraryManagementSystem.Application.DTOs.User.CreateUser;
using LibraryManagementSystem.Application.DTOs.User.Login;
using LibraryManagementSystem.Application.Features.User.CreateUser;
using LibraryManagementSystem.Application.Features.User.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(
    IMediator mediator
) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Create([FromBody] CreateUserRequestDto request, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateUserCommand(request), ct);

        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });

        return Ok(result.Value);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request, CancellationToken ct)
    {
        var result = await mediator.Send(new LoginCommand(request), ct);

        if (!result.IsSuccess)
            return Unauthorized(new { error = result.Error });

        return Ok(result.Value);
    }

}