using Application.Commands;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserEntity>
    {
        private readonly IUserService _userService;




        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
            
        }

        public async Task<UserEntity> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (request.UserImage != null && request.UserImage.Length > 0)
            {
                // Generate a unique file name using Guid
                var fileName = Guid.NewGuid() + Path.GetExtension(request.UserImage.FileName);
                var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserImages");

                // Check if the directory exists, if not, create it
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Combine the directory and file name to get the full file path
                var filePath = Path.Combine(directoryPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.UserImage.CopyToAsync(stream);
                }

                // Set the image URL in the user profile
                request.UserProfile = fileName;
            }

            var user = new UserEntity
            {
                UserName = request.UserName,
                Password = request.Password,
                Email = request.Email,
                FullName = request.FullName,
                Role = request.Role,
                Phone = request.Phone,
                LoginStatus = request.LoginStatus,
                UserProfile = request.UserProfile
            };
            //log the role value to verify it 

            return await _userService.CreateUserAsync(user);
        }
    }
}
