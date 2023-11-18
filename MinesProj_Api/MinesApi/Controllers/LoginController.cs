using Contracts;
using Entities.Models;
using Entities.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MinesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IRepositoryWrapper _repository;

        public LoginController(IConfiguration config, IRepositoryWrapper repository)
        {
            _repository = repository;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LoginCredential loginCredential)
        {
            User? user = await Authenticate(loginCredential);

            if (user != null)
            {
                var token = await GenerateToken(user);
                return Ok(token);
            }
            return NotFound("the specified user was not found.");
        }

        private async Task<User?> Authenticate(LoginCredential loginCredential)
        {
            return await _repository.UserRepo.Get(loginCredential.Username, loginCredential.Password);
        }

        private async Task<string> GenerateToken(User user)
        {
            var role = await _repository.RoleRepo.Get(user.RoleRefId);

            //creating the Issuer signing key declared in program.cs
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credintials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.GivenName, user.GivenName),
                new Claim(ClaimTypes.Surname, user.Surname),
                new Claim(ClaimTypes.Role, role.Title)
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credintials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
