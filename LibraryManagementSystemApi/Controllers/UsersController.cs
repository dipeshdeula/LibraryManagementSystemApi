using Application.Commands;
using Application.DTOs;
using Application.Queries;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMediator _mediator;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, IMediator mediator, ILogger<UsersController> logger)
        {
            _userService = userService;
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<UserEntity>> GetAllUser()
        {
            return await _mediator.Send(new GetAllUsersQuery());
        }

        [HttpGet("{id}")]
        public async Task<UserEntity> GetUserById(int id)
        {
            return await _mediator.Send(new GetUserByIdQuery(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromForm] UsersDto users)
        {
            try
            {
                if (string.IsNullOrEmpty(users.Role))
                {
                    return BadRequest("Role is required.");
                }
                var createdUser = await _mediator.Send(new CreateUserCommand(
                    users.UserName, users.Password, users.Email, string.Empty, users.FullName, users.Phone, users.Role, users.LoginStatus, users.UserImage));

                // Generate the image URL
                var imageUrl = Url.Action("GetUserImage", "Users", new { fileName = createdUser.UserProfile }, Request.Scheme);

                return Ok(
                    new
                    {
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the user.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the user.");
            }
        }

        [HttpGet("GetUserImage")]
        public IActionResult GetUserImage(string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UserImages", fileName);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var image = System.IO.File.OpenRead(filePath);
            return File(image, "image/jpeg");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromForm] UsersDto users)
        {
            if (id != users.UserID)
            {
                return BadRequest("User ID mismatch.");
            }

            try
            {
                var userReturn = await _mediator.Send(new UpdateUserCommand(
                    id,
                    users.UserName,
                    users.Password,
                    users.Email,
                    users.UserProfile,
                    users.FullName,
                    users.Phone,
                    users.Role,
                    users.LoginStatus,
                    users.UserImage));

                return Ok(userReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the user.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the user.");
            }
        }

        [HttpDelete("{id}")]

        public async Task<int> DeleteUser(int id)
        {
            return await _mediator.Send(new DeleteUserCommand { Id = id });
        }
    }

}
