using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAPI.Entities;

namespace UserAPI.Data.Interfaces
{
    public interface IUserContext
    {
        IMongoCollection<User> Users { get; }
    }
}
