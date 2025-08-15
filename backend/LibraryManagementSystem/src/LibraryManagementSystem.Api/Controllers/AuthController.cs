using LibraryManagementSystem.Application.Authentication;
using LibraryManagementSystem.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(
    IJwtTokenService jwtService
) : ControllerBase
{

}