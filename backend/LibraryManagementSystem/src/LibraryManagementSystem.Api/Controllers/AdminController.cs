using LibraryManagementSystem.Application.DTOs.Library.AddAuthor;
using LibraryManagementSystem.Application.DTOs.Library.AddBook;
using LibraryManagementSystem.Application.DTOs.Library.AddGenre;
using LibraryManagementSystem.Application.DTOs.Library.DeleteAuthor;
using LibraryManagementSystem.Application.DTOs.Library.DeleteBook;
using LibraryManagementSystem.Application.DTOs.Library.DeleteGenre;
using LibraryManagementSystem.Application.DTOs.Library.UpdateBook;
using LibraryManagementSystem.Application.DTOs.User.GetUser;
using LibraryManagementSystem.Application.DTOs.User.SaveUserChanges;
using LibraryManagementSystem.Application.Features.Library.AddAuthor;
using LibraryManagementSystem.Application.Features.Library.AddBook;
using LibraryManagementSystem.Application.Features.Library.AddGenre;
using LibraryManagementSystem.Application.Features.Library.DeleteAuthor;
using LibraryManagementSystem.Application.Features.Library.DeleteBook;
using LibraryManagementSystem.Application.Features.Library.DeleteGenre;
using LibraryManagementSystem.Application.Features.Library.GetAllAuthors;
using LibraryManagementSystem.Application.Features.Library.GetAllBooksQuery;
using LibraryManagementSystem.Application.Features.Library.GetAllGenres;
using LibraryManagementSystem.Application.Features.Library.GetBookDetails;
using LibraryManagementSystem.Application.Features.Library.GetUserDetails;
using LibraryManagementSystem.Application.Features.Library.UpdateBook;
using LibraryManagementSystem.Application.Features.User.GetAllUsers;
using LibraryManagementSystem.Application.Features.User.GetUser;
using LibraryManagementSystem.Application.Features.User.SaveChanges;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryManagementSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AdminController(
    IMediator mediator
) : ControllerBase
{
    [HttpPost("author")]
    public async Task<IActionResult> AddAuthor([FromBody] AddAuthorRequestDto request, CancellationToken ct)
    {
        var result = await mediator.Send(new AddAuthorCommand(request), ct);
        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });
        return Ok(result.Value);
    }

    [HttpPost("genre")]
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

    [HttpGet("authors")]
    public async Task<IActionResult> GetAllAuthors(CancellationToken ct)
    {
        var result = await mediator.Send(new GetAllAuthorsQuery(), ct);
        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });
        return Ok(result.Value);
    }

    [HttpGet("genres")]
    public async Task<IActionResult> GetAllGenres(CancellationToken ct)
    {
        var result = await mediator.Send(new GetAllGenresQuery(), ct);
        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });
        return Ok(result.Value);
    }

    [HttpPut("books")]
    public async Task<IActionResult> UpdateBook([FromBody] UpdateBookRequestDto request, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateBookCommand(request), ct);
        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });
        return Ok(result.Value);
    }

    [HttpGet("books/{bookId}")]
    public async Task<IActionResult> GetBookDetails(Guid bookId, CancellationToken ct)
    {
        var result = await mediator.Send(new GetBookDetailsQuery(bookId), ct);
        if (!result.IsSuccess)
            return NotFound(new { error = result.Error });
        return Ok(result.Value);
    }

    [HttpDelete("genre")]
    public async Task<IActionResult> DeleteGenre([FromBody] DeleteGenreRequestDto request, CancellationToken ct)
    {
        var result = await mediator.Send(new DeleteGenreCommand(request), ct);
        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });
        return Ok(new { message = result.Value });
    }

    [HttpDelete("author")]
    public async Task<IActionResult> DeleteAuthor([FromBody] DeleteAuthorRequestDto request, CancellationToken ct)
    {
        var result = await mediator.Send(new DeleteAuthorCommand(request), ct);
        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });
        return Ok(new { message = result.Value });
    }

    [HttpPut("users/save-changes/{userId}")]
    public async Task<IActionResult> SaveChanges(Guid userId, [FromBody] SaveUserChangesRequestDto request, CancellationToken ct)
    {
        var result = await mediator.Send(new SaveUserChangesCommand(userId, request), ct);

        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });

        return Ok(result.Value);
    }
}