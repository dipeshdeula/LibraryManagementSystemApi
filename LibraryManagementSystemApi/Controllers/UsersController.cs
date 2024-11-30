using Application.Commands;
using Application.DTOs;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMediator _mediator;

        public UsersController(IUserService userService, IMediator mediator)
        {
            _userService = userService;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromForm] UsersDto users, IFormFile UserImage)
        {
            var createdUser = await _mediator.Send(new CreateUserCommand(
                 users.UserName,users.Password, users.Email, null, users.FullName, users.Phone, users.Role, false, UserImage));

            //Generate the image url
            var imageUrl = Url.Action("GetUserImage", "Users", new { fileName = createdUser.UserProfile }, Request.Scheme);


            return Ok(
                new { 
                    createdUser.UserID,
                    createdUser.UserName,
                    createdUser.Email,
                    createdUser.Password,
                    createdUser.FullName,
                    createdUser.Phone,
                    createdUser.Role,
                    UserProfile = imageUrl
                });
        }
    }
}
