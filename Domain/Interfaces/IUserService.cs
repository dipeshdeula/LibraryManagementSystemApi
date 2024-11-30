using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserService
    {
        Task<UserEntity> AuthenticateAsync(string username, string password);
        Task<IEnumerable<UserEntity>> GetAllUserAsync();
        Task<UserEntity> GetUserByIdAsync(int id);
        Task<UserEntity>CreateUserAsync(UserEntity user);
        Task UpdateUserAsync(UserEntity user);
        Task DeleteUserAsync(int id);
    }
}
