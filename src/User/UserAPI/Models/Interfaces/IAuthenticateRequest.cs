using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAPI.Entities;

namespace UserAPI.Models.Interfaces
{
    public interface IAuthenticateRequest
    {
        string Username { get; set; }
        string Password { get; set; }
    }
}
