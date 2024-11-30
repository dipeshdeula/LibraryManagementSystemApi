using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class CreateUserCommand : IRequest<UserEntity>
    {
        public CreateUserCommand(string username, string password, string email, string userProfile, string fullName, string phone, string role, bool loginStatus, IFormFile userImage)
        {
            UserName = username;
            Password = password;
            Email = email;
            UserProfile = userProfile;
            FullName = fullName;
            Phone = phone;
            LoginStatus = loginStatus;
            UserImage = userImage;
            
        }

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
