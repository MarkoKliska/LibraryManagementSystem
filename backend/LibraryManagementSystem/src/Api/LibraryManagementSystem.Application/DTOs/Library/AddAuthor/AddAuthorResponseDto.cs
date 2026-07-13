using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.DTOs.Library.AddAuthor;

public record AddAuthorResponseDto
{
    public Guid Id { get; init; }
    public string? FirstName { get; init; }
    public string LastName { get; init; } = default!;
}
