using LibraryManagementSystem.Application.DTOs.User;
using LibraryManagementSystem.Application.Features.User.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController (
    IMediator mediator
) : ControllerBase
{

}
