using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using UserAPI.Data.Interfaces;
using UserAPI.Entities;
using UserAPI.Helpers;
using UserAPI.Models;
using UserAPI.Repositories.Interfaces;

namespace UserAPI.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly IUserContext _context;

        public UserRepository(IUserContext context)
        {
            _context = context;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            var user = await _context.Users.Find(p => p.Username == model.Username && p.Password == model.Password).FirstOrDefaultAsync();
            if(user == null)
            {
                return null;
            }

            return new AuthenticateResponse(user, TokenGenerator.GenerateJwtToken(user));
        }

        public async Task Create(User user)
        {
            await _context.Users.InsertOneAsync(user);
        }

        public async Task<bool> Delete(string id)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq(p => p.Id, id);
            DeleteResult deleteResult = await _context.Users.DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<User> GetUser(string id)
        {
            return await _context.Users.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.Find(p => true).ToListAsync();
        }

        public async Task<bool> Update(User user)
        {
            var updateResult = await _context.Users.ReplaceOneAsync(filter: g => g.Id == user.Id, replacement: user);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
