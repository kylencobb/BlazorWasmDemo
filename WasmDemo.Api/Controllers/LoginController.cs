using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WasmDemo.Shared;

namespace WasmDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        
        public async Task<LoginResult> SignIn(LoginModel loginModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("Model invalid");

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, loginModel.Username),
                    new Claim(ClaimTypes.Role, "Administrator")
                };

                var expires = DateTime.Now.AddDays(1);
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecurityKey"]));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                //demo token
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: expires,
                    signingCredentials: credentials
                );

                return new LoginResult
                {
                    IsSuccess = true,
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                };
            }
            catch (Exception ex)
            {
                //log/handle
                return new LoginResult { IsSuccess = false };
            }
        } 
    }
}
