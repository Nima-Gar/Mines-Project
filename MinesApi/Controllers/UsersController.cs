using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MinesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repository;

        public UsersController(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/User/GetUser?id=5
        [HttpGet(nameof(GetUser))]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _repository.UserRepo.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [AllowAnonymous]
        [HttpPost(nameof(PostUser))]
        public async Task<ActionResult<User>> PostUser([FromBody] UserViewModel user)
        {

            User userData = _mapper.Map<User>(user);

            await _repository.UserRepo.Add(userData);

            return CreatedAtAction("GetUser", new { id = userData.Id }, userData);
        }
    }
}
