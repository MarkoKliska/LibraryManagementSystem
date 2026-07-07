using LibraryManagementSystem.Application.DTOs.Library.GetRentedBooks;
using LibraryManagementSystem.Application.DTOs.Library.RentBook;
using LibraryManagementSystem.Application.DTOs.Library.ReturnBook;
using LibraryManagementSystem.Application.DTOs.Library.SearchBook;
using LibraryManagementSystem.Application.Features.Library.GetAllBooksQuery;
using LibraryManagementSystem.Application.Features.Library.GetRentedBooks;
using LibraryManagementSystem.Application.Features.Library.RentBook;
using LibraryManagementSystem.Application.Features.Library.ReturnBook;
using LibraryManagementSystem.Application.Features.Library.SearchBook;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryManagementSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BookController(
    IMediator mediator
) : ControllerBase
{
    [HttpGet("all")]
    public async Task<IActionResult> GetAllBooks(CancellationToken ct)
    {
        var result = await mediator.Send(new GetAllBooksQuery(), ct);

        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });

        return Ok(result.Value);
    }

    [HttpGet("rented")]
    public async Task<IActionResult> GetRentedBooks(CancellationToken ct)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            return Unauthorized(new { error = "Unauthorized" });

        var request = new GetRentedBooksRequestDto { UserId = userId };
        var result = await mediator.Send(new GetRentedBooksQuery(request), ct);

        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });

        return Ok(result.Value);
    }

    [Authorize(Roles = "User")]
    [HttpPost("rent")]
    public async Task<IActionResult> RentBook([FromBody] RentBookRequestDto request, CancellationToken ct)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            return Unauthorized(new { error = "Unauthorized" });

        request = request with { UserId = userId };
        var result = await mediator.Send(new RentBookCommand(request), ct);

        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });

        return Ok(result.Value);
    }

    [Authorize(Roles = "User")]
    [HttpPost("return")]
    public async Task<IActionResult> ReturnBook([FromBody] ReturnBookRequestDto request, CancellationToken ct)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            return Unauthorized(new { error = "Unauthorized" });

        request = request with { UserId = userId };
        var result = await mediator.Send(new ReturnBookCommand(request), ct);

        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });

        return Ok(result.Value);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchBooks([FromQuery] SearchBooksRequestDto request, CancellationToken ct)
    {
        var result = await mediator.Send(new SearchBooksQuery(request), ct);
        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });
        return Ok(result.Value);
    }
}
