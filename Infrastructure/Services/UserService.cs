using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserEntity> AuthenticateAsync(string username, string password)
        {
            return await _userRepository.AuthenticateAsync(username, password);
        }

        public async Task<IEnumerable<UserEntity>> GetAllUserAsync()
        {
            return await _userRepository.GetAllUserAsync();

        }

        public async Task<UserEntity> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<UserEntity> CreateUserAsync(UserEntity user)
        {
            await _userRepository.AddUserAsync(user);
            return user;
        }

        public async Task UpdateUserAsync(UserEntity user, IFormFile? userImage =null)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(user.UserID);
            if(existingUser == null)
            {

                throw new Exception("User not found");
            }
            existingUser.UserName = user.UserName ?? existingUser.UserName;
            existingUser.Password = user.Password ?? existingUser.Password;
            existingUser.Email = user.Email ?? existingUser.Email;
            existingUser.UserProfile = user.UserProfile ?? existingUser.UserProfile;
            existingUser.FullName = user.FullName ?? existingUser.FullName;
            existingUser.Phone = user.Phone ?? existingUser.Phone;
            existingUser.Role = user.Role ?? existingUser.Role;
            existingUser.LoginStatus = user.LoginStatus;

            //Handle the UserImage property
            if (userImage != null)
            {
                //save the image to a file or database and update the AuthorProfile property
                var imagePath = SaveUserImage(userImage);
                existingUser.UserProfile = imagePath;
            }

            await _userRepository.UpdateUserAsync(user);
        }

        private string SaveUserImage(IFormFile userImage)
        {
            // Implement the logic to save the image and return the file path or URL
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UserImages", userImage.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                userImage.CopyTo(stream);
            }
            return filePath;
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteUserAsync(id);
        }
    
    }
}
