using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class UpdateUserCommand : IRequest<int>
    {
        public UpdateUserCommand(int id,string username, string password, string email, string userProfile, string fullName, string phone, string role, bool loginStatus, IFormFile userImage)
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

        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string UserProfile { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Role { get; set; } = null!;

        public bool LoginStatus { get; set; } = false;
        public IFormFile UserImage { get; set; }

    }
}
