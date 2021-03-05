using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAPI.Entities;

namespace UserAPI.Models.Interfaces
{
    public interface IAuthenticateResponse
    {
        string Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Username { get; set; }
        DateTime DateOfBirth { get; set; }
        string ProfilePicture { get; set; }
        Role Role { get; set; }
        string Token { get; set; }
    }
}
