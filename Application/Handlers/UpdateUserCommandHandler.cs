using Application.Commands;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, int>
    {
        private readonly IUserService _userService;

        public UpdateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<int> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByIdAsync(request.UserId);
            if (user == null)
            {
                throw new Exception("User not found.");

            }

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

            //user.UserID = request.UserId;
            user.UserName = request.UserName ?? user.UserName;
            user.Password = request.Password ?? user.Password;
            user.Email = request.Email ?? user.Email;
            user.FullName = request.FullName ?? user.FullName;
            user.UserProfile = request.UserProfile ?? user.UserProfile;
            user.Role = request.Role ?? user.Role;
            user.Phone = request.Phone ?? user.Phone;
            user.LoginStatus = request.LoginStatus;

            await _userService.UpdateUserAsync(user);
            return user.UserID;


        }
    }
}
