using LibraryManagementSystem.Application.DTOs.User;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController (
    IUserRepository userRepository
) : ControllerBase
{

}
