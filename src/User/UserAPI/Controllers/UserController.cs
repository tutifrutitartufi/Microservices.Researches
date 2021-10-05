using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UserAPI.Entities;
using UserAPI.Models;
using UserAPI.Repositories.Interfaces;

namespace UserAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("authenticate")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(AuthenticateResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AuthenticateResponse>> LogIn([FromBody] AuthenticateRequest model)
        {
            var payload = await _repository.Authenticate(model);
            if(payload == null)
            {
                return BadRequest("Не постоји корисник са унетим креденцијалима.");
            }
            return Ok(payload);
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<User>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _repository.GetUsers();
            return Ok(users);
        }
        [HttpGet("{id:length(24)}", Name = "GetUser")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<User>> GetUserByID(string id)
        {
            var user = await _repository.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpGet("search")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<User>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<User>>> SearchUser(string username)
        {
            var users = await _repository.Search(username);
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }
        [HttpPost]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<User>> CreateUser([FromBody] User user)
        {
            try
            {
                await _repository.Create(user);
                return Ok("Кориснички налог је успешно креиран");
            }
            catch
            {
                return BadRequest("Кориснички налог није креиран");
            }
        }
        [HttpPut]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateUser([FromBody] User user)
        {
            if(await _repository.Update(user))
            {
                return Ok("Успешно сте изменили кориснички налог");
            } else
            {
                return BadRequest("Није могуће изменити кориснички налог");
            }
        }
        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteUser(string id)
        {
            if(await _repository.Delete(id))
            {
                return Ok("Успешно сте обрисали кориснички налог");
            }
            else
            {
                return BadRequest("Није могуће обрисати кориснички налог");
            }
        }

    }
}
