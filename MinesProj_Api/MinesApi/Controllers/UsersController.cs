using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinesApi.Common;
using MinesApi.Models;
using System.Security.Claims;

namespace MinesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repository;

        public UsersController(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/Users/GetUsers
        [HttpGet(nameof(GetUsers))]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _repository.UserRepo.GetAll();

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        // GET: api/Users/GetUser?id=5
        [HttpGet(nameof(GetUser))]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _repository.UserRepo.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet(nameof(GetCurrentUser))]
        public ActionResult<UserClaims?> GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                return Ok(HelperMethods.GetCurrentUserClaims(identity));
            }
            return NotFound();
        }

        // POST: api/Users/PostUser
        [HttpPost(nameof(PostUser))]
        public async Task<ActionResult<User>> PostUser([FromBody] UserViewModel user)
        {
            if (! _repository.UserRepo.usernameAlreadyExists(user.Username))
            {
                User userData = _mapper.Map<User>(user);

                await _repository.UserRepo.Add(userData);

                return CreatedAtAction("GetUser", new { id = userData.Id }, userData);
            }
            return Conflict();
        }

        [HttpDelete(nameof(DeleteUser))]
        public async Task<ActionResult> DeleteUser(int id)
        {
            User? user = await _repository.UserRepo.Get(id);
            if (user == null)
            {
                return NotFound();
            }

            await _repository.UserRepo.Delete(user.Id);

            return NoContent();
        }

    }
}
