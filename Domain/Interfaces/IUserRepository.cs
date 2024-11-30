using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity> AuthenticateAsync(string username,string password);
        Task<IEnumerable<UserEntity>> GetAllUserAsync();
        Task<UserEntity> GetUserByIdAsync(int id);
        Task<string> AddUserAsync(UserEntity user);
        Task<string> UpdateUserAsync(UserEntity user);
        Task<string> DeleteUserAsync(int id);

        
    }
}
