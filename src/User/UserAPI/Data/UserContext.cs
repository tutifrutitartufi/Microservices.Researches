using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAPI.Data.Interfaces;
using UserAPI.Entities;
using UserAPI.Settings;

namespace UserAPI.Data
{
    public class UserContext : IUserContext
    {
        public UserContext(IUserDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Users = database.GetCollection<User>(settings.CollectionName);
            UserContextSeed.SeedData(Users);
        }

        public IMongoCollection<User> Users { get; }

    }
}
