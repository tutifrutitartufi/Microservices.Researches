using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAPI.Entities;
using UserAPI.Models;

namespace UserAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(string id);
        Task<IEnumerable<User>> Search(string criteria);
        Task Create(User user);
        Task<bool> Update(User user);
        Task<bool> Delete(string id);
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);

    }
}
