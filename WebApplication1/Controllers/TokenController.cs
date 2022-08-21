using MoviesAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MoviesAPI.Data.Entities;
using MoviesAPI.Utils;

namespace JWTAuth.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly MovieDBContext _context;
        private Utilities utils;

        private readonly ILogger<TokenController> log;

        public TokenController(IConfiguration config, MovieDBContext context, ILogger<TokenController> logger)
        {
            _configuration = config;
            _context = context;
            log = logger;
            utils = new Utilities();
        }

        [HttpPost]
        public async Task<IActionResult> GenerateToken(UserInfo _userData)
        {
            log.LogInformation("Generating Token");
            if (_userData != null && _userData.Email != null && _userData.Password != null)
            {
                var secretKey = _configuration["SecretKey"];

                log.LogInformation("Generating HMAC-SHA256 Hash");
                _userData.Password = utils.GetSHA256HMACHash(_userData.Password, secretKey);

                log.LogInformation("Getting User");
                var user = await GetUser(_userData.Email, _userData.Password);

                if (user != null)
                {
                    log.LogInformation("User found, generating Token.");
                    //create claims details based on the user information
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.UserId.ToString()),
                        new Claim("DisplayName", user?.DisplayName),
                        new Claim("UserName", user?.UserName),
                        new Claim("Email", user?.Email),
                        new Claim(ClaimTypes.Role, user?.Role)
                };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddYears(1), //SET INTENTIONALLY TO 1 YEAR FOR TESTING
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    log.LogInformation("Invalid User");
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                log.LogInformation("Invalid Request");
                return BadRequest();
            }
        }

        private async Task<UserInfo?> GetUser(string email, string password)
        {
            return await _context.UserInfo.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }
    }
}