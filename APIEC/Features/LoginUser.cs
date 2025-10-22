//using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using APIEC.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace APIEC.Features
{
    public static class LoginUser
    {
        public record Request (string email, string password);

        //public static void MapEndpoint(IEndpointRouteBuilder app)
        //{
        //    app.MapPost("login", async (
        //        Request request, 
        //        UserManager<EmployeeEntity> userManager,
        //        // i can also use options pattern
        //        IConfiguration configuration
        //        ) =>
        //    {
        //        // generate JWT
        //        var user = await userManager.FindByEmailAsync(request.email);

        //        if (user is null) {
        //            return Results.Unauthorized();
        //        }

        //        var roles = await userManager.GetRolesAsync(user);

        //        var signingKey = new SymmetricSecurityKey(
        //            Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]!));
        //        var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        //        List<Claim> claims = [
        //            new (JwtRegisteredClaimNames.Sub, user.Name!),
        //            new (JwtRegisteredClaimNames.Email, user.Email!),
        //            ..roles.Select(r => new Claim(ClaimTypes.Role, r))
        //            ];

        //        var tokenDescriptor = new SecurityTokenDescriptor
        //        {
        //            Subject = new ClaimsIdentity(claims),
        //            Expires = DateTime.UtcNow.
        //            AddMinutes(configuration.GetValue<int>("JWT:ExpirationInMinutes")),
        //            SigningCredentials = credentials,
        //            Issuer = configuration["JWT:Issuer"],
        //            Audience = configuration["JWT:Audience"]
        //        };

        //        var tokenHandler = new JsonWebTokenHandler();

        //        string accessToken = tokenHandler.CreateToken(tokenDescriptor);
        //        return Results.Ok(new { AccessToken = accessToken, User = user });
        //    });
        //}
    }
}
