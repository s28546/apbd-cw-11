using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTO;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/auth")]
public class ApiController : ControllerBase
{
    [HttpPost]
    public IActionResult Login(LoginDTO loginDTO)
    {
        /*
         *   sprwadzam  czy istnieje user o podanym emailu i czy zgadza sie haslo
         */

        Claim[] userClaims =
        {
            new Claim(ClaimTypes.Email, "email usera"),
            new Claim(ClaimTypes.NameIdentifier, "1")
        };
        
        /* klucz ktorym podpisujemy token, powinien byc zapisany w configu */
        string secret = "asdasdasdasdasgfgrewecxzczbherewr";
        
        
        /* paczka Microsoft.AspNetCore.Authentication.JwtBearer*/
        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(secret));
        SigningCredentials signingCredentials = new(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken jwtSecurityToken = new(
            issuer: "https://localhost:5000",
            audience: "https://localhost:5002",
            claims: userClaims,
            expires DateTime.Now.AddMinutes(10),
            signingCredentials: signingCredentials
        );

        Guid refreshToken = Guid.NewGuid().ToString();
        DateTime refreshTokenExpires = DateTime.Now.AddHours(1);

        string accessToken = JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        AuthenticatedDTO authenticatedDto = new(accessToken, refreshToken, refreshTokenExpires);
        
        return Ok();
    }
}