using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.UsersCommand
{
    public class UpdateUserCommand : IRequest<int>
    {
        public UpdateUserCommand(int id, string? username = null, string? password = null, string? email = null, string? userProfile = null, string? fullName = null, string? phone = null, string? role = null, bool loginStatus = false, IFormFile? userImage = null)
        {
            UserId = id;
            UserName = username;
            Password = password;
            Email = email;
            UserProfile = userProfile;
            FullName = fullName;
            Phone = phone;
            Role = role;
            LoginStatus = loginStatus;
            UserImage = userImage;

        }
        public int UserId { get; set; }

        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }

        public string? UserProfile { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public string? Role { get; set; }

        public bool LoginStatus { get; set; } = false;
        public IFormFile? UserImage { get; set; }

    }
}
