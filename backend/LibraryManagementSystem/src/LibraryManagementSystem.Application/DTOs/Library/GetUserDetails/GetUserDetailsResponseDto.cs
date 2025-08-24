namespace LibraryManagementSystem.Application.DTOs.Library.GetUserDetails;

public record GetUserDetailsResponseDto
{
    public Guid Id { get; init; }
    public string FirstName { get; init; } = default!;
    public string LastName { get; init; } = default!;
    public string Email { get; init; } = default!;
    public IEnumerable<RentalDetailsDto> Rentals { get; init; } = new List<RentalDetailsDto>();
}