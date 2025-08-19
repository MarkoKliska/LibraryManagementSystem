using LibraryManagementSystem.Application.DTOs.User;
using LibraryManagementSystem.Application.DTOs.User.GetUser;
using LibraryManagementSystem.Application.DTOs.User.SaveUserChanges;
using LibraryManagementSystem.Application.Features.User.GetUser;
using LibraryManagementSystem.Application.Features.User.SaveChanges;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryManagementSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController (
    IMediator mediator
) : ControllerBase
{
    [HttpGet("get-user")]
    public async Task<IActionResult> GetMyInfo(CancellationToken ct)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            return Unauthorized(new { error = "Unauthorized" });

        var request = new GetUserRequestDto
        {
            UserId = userId
        };

        var result = await mediator.Send(new GetUserQuery(request), ct);

        if (!result.IsSuccess)
            return NotFound(new { error = result.Error });

        return Ok(result.Value);
    }

    [HttpPut("save-changes")]
    public async Task<IActionResult> SaveChanges([FromBody] SaveUserChangesRequestDto request, CancellationToken ct)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            return Unauthorized(new { error = "Unauthorized" });

        var result = await mediator.Send(new SaveUserChangesCommand(userId, request), ct);

        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });

        return Ok(result.Value);
    }
}