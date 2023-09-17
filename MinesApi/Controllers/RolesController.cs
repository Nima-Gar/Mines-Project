using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace MinesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repository;

        public RolesController(IMapper mapper, IRepositoryWrapper repository)
        {
            _mapper = mapper;
            _repository = repository;
        }


        // GET: api/Roles/GetRole?id=5
        [HttpGet(nameof(GetRole))]
        public async Task<ActionResult<Role>> GetRole(int id)
        {
            var role = await _repository.RoleRepo.Get(id);

            if (role == null)
            {
                return NotFound();
            }

            return role;
        }
    }
}
