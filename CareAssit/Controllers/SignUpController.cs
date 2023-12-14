using CareAssit.Data;
using CareAssit.Models.Domain;
using CareAssit.Models.DTO;
using CareAssit.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CareAssit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private readonly CareAssitDbContext dbContext;
        private readonly ISignUpRepository signUpRepository;
        private readonly IUserRepository userRepository;
        private bool isAdmin = false;
        private bool isCompany = false;
        private bool isHealth = false;
        public SignUpController(CareAssitDbContext dbContext, ISignUpRepository signUpRepository, IUserRepository userRepository)
        {
            this.dbContext = dbContext;
            this.signUpRepository = signUpRepository;
            this.userRepository = userRepository;
        }
        [HttpPost]
        [Route("UserStoreSignup")]
        public async Task<IActionResult> CreateSignUpForUser([FromBody] AddSignUpDto addSignUpDto)
        {
            var existingUser = await dbContext.SignUps.FirstOrDefaultAsync(x => x.Email == addSignUpDto.Email);
            if (existingUser != null)
            {
                return BadRequest("Email is Already Present.Please Login");
            }
            var signUpModel = new SignUp
            {
                Email = addSignUpDto.Email,
                Password = addSignUpDto.Password,
                User_Name = addSignUpDto.User_Name,
            };
            signUpModel = await signUpRepository.CreateAsync(signUpModel);
            var userDomainModel = new User
            {
                Name = addSignUpDto.User_Name,
                Password = addSignUpDto.Password,
                Email = addSignUpDto.Email,
            };
            userDomainModel = await userRepository.CreateAsync(userDomainModel);
            return Ok(userDomainModel);
        }
        [HttpPost]
        [Route("StoreSignup")]
        public async Task<IActionResult> CreateSignup([FromBody] AddSignUpDto addSignUpDto)
        {
            var existingUser = await dbContext.SignUps.FirstOrDefaultAsync(x => x.Email == addSignUpDto.Email);
            if (existingUser != null)
            {
                return BadRequest("Email is Already Present.Please Login");
            }
            var signUpModel = new SignUp
            {
                Email = addSignUpDto.Email,
                Password = addSignUpDto.Password,
                User_Name = addSignUpDto.User_Name,
            };
            signUpModel = await signUpRepository.CreateAsync(signUpModel);
            return Ok(signUpModel);
        }
        [HttpPost]
        [Route("checkemailexist")]
        public async Task<IActionResult> CheckEmailExist(string email)
        {
            var user = await dbContext.SignUps.FirstOrDefaultAsync(x => x.Email == email);
            if (user != null)
            {
                return Ok(1);
            }
            return NotFound();
        }
    }
}