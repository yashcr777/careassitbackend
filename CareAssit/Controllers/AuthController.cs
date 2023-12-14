/*using CareAssit.Models.Domain;
using CareAssit.Models.DTO;
using CareAssit.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CareAssit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegistorRequestDto registorRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registorRequestDto.UserName,
                Email = registorRequestDto.UserName,
            };
            *//*var identityUser = new User
            {
                UserName = registorRequestDto.UserName,
                Email = registorRequestDto.UserName,
            };*//*
            var identityResult = await userManager.CreateAsync(identityUser, registorRequestDto.Password);
            if (identityResult.Succeeded)
            {
                if (registorRequestDto.Roles != null && registorRequestDto.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registorRequestDto.Roles);
                    if (identityResult.Succeeded)
                    {

                        return Ok("User was registered! Please Login.");
                    }
                }
            }
            return BadRequest("Something Went Wrong");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.UserName);
            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (checkPasswordResult)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());
                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };
                        return Ok(response);
                    }
                }
            }
            return BadRequest("Username or Password Incorrect");
        }
    }
}*/