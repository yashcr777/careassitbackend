using CareAssit.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CareAssit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly CareAssitDbContext dbContext;
        private readonly IConfiguration configuration;

        public LoginController(CareAssitDbContext dbContext, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.configuration = configuration;
        }
        [HttpGet("Checkforhealthprovider")]
        public async Task<bool> checkHealthProvider(string email)
        {
            var healthProvider = await dbContext.HealthProviders.FirstOrDefaultAsync(x => x.Email == email);
            if (healthProvider == null)
            {
                return false;
            }
            return true;
        }
        [HttpGet]
        [Route("Checkforinsurancecompany")]
        public async Task<bool> checkInsuranceCompany(string email)
        {
            var insuranceCompany = await dbContext.InsuranceCompanies.FirstOrDefaultAsync(x => x.Email == email);
            if (insuranceCompany == null)
            {
                return false;
            }
            return true;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Postmethoud(string email, string password)
        {
            var user = await dbContext.SignUps.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
            if (user == null)
            {
                return Unauthorized();
            }
            var user1 = await dbContext.Users.FirstOrDefaultAsync(x=>x.Email==email && x.Password==password);
            var user2 = await dbContext.HealthProviders.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
            var user3 = await dbContext.InsuranceCompanies.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
            
            Guid user4;
            if(user1!=null)
            {
                user4 = user1.User_Id;
            }
            else if(user2!=null) 
            {
                user4 = user2.Health_Id;
            }
            else
            {
                user4 = user3.InsuranceCompany_Id;
            }
            
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
            var signingCredentials = new SigningCredentials(
                                    new SymmetricSecurityKey(key),
                                    SecurityAlgorithms.HmacSha512Signature);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, email),
            };
            string userRole = null;


            if (email == "yashagarwal@gmail.com")
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                userRole = "Admin";
            }
            else if (await checkHealthProvider(email))
            {
                claims.Add(new Claim(ClaimTypes.Role, "HealthProvider"));
                userRole = "HealthProvider";

            }
            else if (await checkInsuranceCompany(email))
            {
                claims.Add(new Claim(ClaimTypes.Role, "InsuranceCompany"));
                userRole = "InsuranceCompany";
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, "User"));
                userRole = "User";
            }
            var expires = DateTime.UtcNow.AddMinutes(20);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(20),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = signingCredentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            return Ok(new { Token = jwtToken, Role = userRole,id= user4});
        }

    }
}
