using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using MinesApi.Models.ViewModels;

namespace MinesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repository;

        public RolesController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        // GET: api/Roles/GetRoles
        [HttpGet(nameof(GetRoles))]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            var roles = await _repository.RoleRepo.GetAll();
            if (roles == null)
            {
                return NotFound();
            }

            return Ok(roles);
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
