using LibraryManagementSystem.Application.DTOs.Admin.AddAuthor;
using LibraryManagementSystem.Application.DTOs.Admin.AddBook;
using LibraryManagementSystem.Application.DTOs.Admin.AddGenre;
using LibraryManagementSystem.Application.DTOs.Admin.DeleteBook;
using LibraryManagementSystem.Application.Features.Admin.AddAuthor;
using LibraryManagementSystem.Application.Features.Admin.AddBook;
using LibraryManagementSystem.Application.Features.Admin.AddGenre;
using LibraryManagementSystem.Application.Features.Admin.DeleteBook;
using LibraryManagementSystem.Application.Features.Admin.GetAllBooksQuery;
using LibraryManagementSystem.Application.Features.Admin.GetUserDetails;
using LibraryManagementSystem.Application.Features.User.GetAllUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController(
    IMediator mediator
) : ControllerBase
{
    [HttpPost("authors")]
    public async Task<IActionResult> AddAuthor([FromBody] AddAuthorRequestDto request, CancellationToken ct)
    {
        var result = await mediator.Send(new AddAuthorCommand(request), ct);
        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });
        return Ok(result.Value);
    }

    [HttpPost("genres")]
    public async Task<IActionResult> AddGenre([FromBody] AddGenreRequestDto request, CancellationToken ct)
    {
        var result = await mediator.Send(new AddGenreCommand(request), ct);
        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });
        return Ok(result.Value);
    }

    [HttpPost("books")]
    public async Task<IActionResult> AddBook([FromBody] AddBookRequestDto request, CancellationToken ct)
    {
        var result = await mediator.Send(new AddBookCommand(request), ct);
        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });
        return Ok(result.Value);
    }

    [HttpGet("books")]
    public async Task<IActionResult> GetAllBooks(CancellationToken ct)
    {
        var result = await mediator.Send(new GetAllBooksQuery(), ct);
        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });
        return Ok(result.Value);
    }

    [HttpDelete("books")]
    public async Task<IActionResult> DeleteBook([FromBody] DeleteBookRequestDto request, CancellationToken ct)
    {
        var result = await mediator.Send(new DeleteBookCommand(request), ct);
        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });
        return Ok(new { message = result.Value });
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers(CancellationToken ct)
    {
        var result = await mediator.Send(new GetAllUsersQuery(), ct);
        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });
        return Ok(result.Value);
    }

    [HttpGet("users/{userId}")]
    public async Task<IActionResult> GetUserDetails(Guid userId, CancellationToken ct)
    {
        var result = await mediator.Send(new GetUserDetailsQuery(userId), ct);
        if (!result.IsSuccess)
            return NotFound(new { error = result.Error });
        return Ok(result.Value);
    }
}
