using APIEC.Features;
using APIEC.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System;
using System.Linq;

namespace APIEC.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<EmployeeEntity> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<EmployeeEntity> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUser.Request request)
        {
            var user = await _userManager.FindByEmailAsync(request.email);
            if (user is null)
                return Unauthorized();

            // generate JWT logic here

            var roles = await _userManager.GetRolesAsync(user);

            var signingKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]!));
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Name ?? ""),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? "")
            };

            // add roles to claims
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));


            //List<Claim> claims = [
            //    new (JwtRegisteredClaimNames.Sub, user.Name!),
            //        new (JwtRegisteredClaimNames.Email, user.Email!),
            //        ..roles.Select(r => new Claim(ClaimTypes.Role, r))
            //    ];

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.
                AddMinutes(_configuration.GetValue<int>("JWT:ExpirationInMinutes")),
                SigningCredentials = credentials,
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"]
            };

            var tokenHandler = new JsonWebTokenHandler();

            string accessToken = tokenHandler.CreateToken(tokenDescriptor);
            
            return Ok(new { Token = accessToken });
        }
    }

    //public class AuthController : Controller
    //{
    //    public IActionResult Index()
    //    {
    //        return View();
    //    }
    //}
}
