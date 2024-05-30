using Azure.Core;
using buibaquang_aspcoreblazor.Api.Data;
using buibaquang_aspcoreblazor.Api.Entities;
using buibaquang_aspcoreblazor.Api.Repositories;
using buibaquang_aspcoreblazor.Models.Login;
using buibaquang_aspcoreblazor.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Polly;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace buibaquang_aspcoreblazor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IAuthRepository _authRepository;
        private readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public AuthController(IConfiguration configuration,
                                UserManager<User> userManager,
                                SignInManager<User> signInManager,
                                IAuthRepository authRepository)
        {
            _configuration = configuration;
            _signInManager = signInManager;
            _userManager = userManager;
            _authRepository = authRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            var user = await _userManager.FindByNameAsync(login.UserName);
            if (user == null) return BadRequest(new LoginResponse { Successful = false, Error = "Username and password are invalid." });

            var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, false, false);

            if (!result.Succeeded) return BadRequest(new LoginResponse { Successful = false, Error = "Username and password are invalid." });

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, login.UserName),
                new Claim("UserId", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtExpiryInDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtAudience"],
                claims,
                expires: expiry,
                signingCredentials: creds
            );

            return Ok(new LoginResponse { Successful = true, Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest reqUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = reqUser.FirstName,
                LastName = reqUser.LastName,
                UserName = reqUser.UserName,
                Email = reqUser.Email,
                PhoneNumber = reqUser.PhoneNumber,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, reqUser.Password == null ? "Aa@123" : reqUser.Password);

            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(user);
        }
    }
}
