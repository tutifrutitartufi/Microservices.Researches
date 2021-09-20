using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using UserAPI.Data.Interfaces;
using UserAPI.Entities;
using UserAPI.Helpers;
using UserAPI.Models;
using UserAPI.Repositories.Interfaces;
using BC = BCrypt.Net.BCrypt;

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
            var user = await _context.Users.Find(p => p.Username == model.Username).FirstOrDefaultAsync();
                if (user == null || !BC.Verify(model.Password, user.Password))
                {
                    return null;
                }
               
                return new AuthenticateResponse(user, TokenGenerator.GenerateJwtToken(user));
        }

        public async Task Create(User user)
        {
            string passwordHash = BC.HashPassword(user.Password);
            user.Password = passwordHash;
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
        public async Task<IEnumerable<User>> Search(string username)
        {
            var filter = new BsonDocument { { "Username", new BsonDocument { { "$regex", username }, { "$options", "i" } } } };
            return await _context.Users.Find(filter).ToListAsync();
        }

        public async Task<bool> Update(User user)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq(p => p.Id, user.Id);
            var update = Builders<User>.Update
                .Set("FirstName", user.FirstName)
                .Set("LastName", user.LastName)
                .Set("Username", user.Username)
                .Set("ProfilePicture", user.ProfilePicture)
                .Set("DateOfBirth", user.DateOfBirth)
                .Set("Role", user.Role);
            var updateResult = await _context.Users.UpdateOneAsync(filter, update);
            return updateResult.ModifiedCount == 1;

        }
    }
}
