using AutoMapper;
using CareAssit.Data;
using CareAssit.Models.Domain;
using CareAssit.Models.DTO;
using CareAssit.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.Security.Claims;
using Claim = CareAssit.Models.Domain.Claim;

namespace CareAssit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly CareAssitDbContext dbContext;
        private readonly IUserRepository userRepository;
        private readonly ILogger<UserController> logger;
        private readonly IMapper mapper;
        private readonly IRequestRepository requestRepository;
        private readonly IInvoiceRepository invoiceRepository;
        private readonly IClaimRepository claimRepository;

        public UserController(CareAssitDbContext dbContext,IUserRepository userRepository, ILogger<UserController> logger, IMapper mapper, IRequestRepository requestRepository,IInvoiceRepository invoiceRepository,IClaimRepository claimRepository)
        {
            this.dbContext = dbContext;
            this.userRepository = userRepository;
            this.logger = logger;
            this.mapper = mapper;
            this.requestRepository = requestRepository;
            this.invoiceRepository = invoiceRepository;
            this.claimRepository = claimRepository;
            this.userRepository = userRepository;
        }

        [HttpGet]
        //[Authorize(Roles ="User")]
        public async Task<IActionResult> GetAll()
        {
            var userDomainModel = await userRepository.GetAllAsync();
            //Map Domain Model To Dto
            return Ok(mapper.Map<List<UserDto>>(userDomainModel));
        }
        // Get User By Id
        [HttpGet]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "User")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var userDomain = await userRepository.GetByIdAsync(id);
            if (userDomain == null)
            {
                return NotFound();
            }
            var userDto = new UserDto
            {
                User_Id = userDomain.User_Id,
                Name = userDomain.Name,
                Email=userDomain.Email,
                Password = userDomain.Password,
                ContactNumber = userDomain.ContactNumber,
                Dob = userDomain.Dob,
                Gender = userDomain.Gender,
                Description = userDomain.Description,
                Address = userDomain.Address,
                Blood_Group= userDomain.Blood_Group,
            };
            return Ok(userDto);
        }
        // Create User
        [HttpPost]
        //[Authorize(Roles = "User,HealthProvider")]
        public async Task<IActionResult> Create([FromBody] AdduserDto adduserDto)
        {
            var userDomainModel = new User
            {
                Name = adduserDto.Name,
                ContactNumber = adduserDto.ContactNumber,
                Gender = adduserDto.Gender,
                Description = adduserDto.Description,
                Address = adduserDto.Address,
                Dob = adduserDto.Dob,
                Blood_Group = adduserDto.Blood_Group,
            };
            userDomainModel = await userRepository.CreateAsync(userDomainModel);

            var userDto = new UserDto
            {
                User_Id = userDomainModel.User_Id,
                Name = userDomainModel.Name,
                ContactNumber = userDomainModel.ContactNumber,
                Dob = userDomainModel.Dob,
                Gender = userDomainModel.Gender,
                Description = userDomainModel.Description,
                Address = userDomainModel.Address,
                Blood_Group = userDomainModel.Blood_Group,
            };
            return CreatedAtAction(nameof(GetById), new { id = userDto.User_Id }, userDto);
        }
        // Update the User
        [HttpPut]
        [Authorize(Roles = "User")]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateUserDto updateUserDto)
        {
            var userDomainModel = new User
            {
                Name = updateUserDto.Name,
                ContactNumber = updateUserDto.ContactNumber,
                Dob = updateUserDto.Dob,
                Gender = updateUserDto.Gender,
                Description = updateUserDto.Description,
                Address = updateUserDto.Address,
                Blood_Group= updateUserDto.Blood_Group,
            };
            userDomainModel = await userRepository.UpdateAsync(id, userDomainModel);
            if (userDomainModel == null)
            {
                return NotFound();
            }
            var userDto = new UserDto
            {
                User_Id = userDomainModel.User_Id,
                Name = userDomainModel.Name,
                Email = userDomainModel.Email,
                Password = userDomainModel.Password,
                ContactNumber = userDomainModel.ContactNumber,
                Dob = userDomainModel.Dob,
                Gender = userDomainModel.Gender,
                Description = userDomainModel.Description,
                Address = userDomainModel.Address,
                Blood_Group= userDomainModel.Blood_Group,
            };
            return Ok(userDto);
        }

        // Delete The User
        [HttpDelete]
        //[Authorize(Roles = "HealthProvider")]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var userDomian = await userRepository.DeleteAsync(id);
            if (userDomian == null)
            {
                return NotFound();
            }
            var userDto = new UserDto
            {
                User_Id = userDomian.User_Id,
                Name = userDomian.Name,
                ContactNumber = userDomian.ContactNumber,
                Dob = userDomian.Dob,
                Gender = userDomian.Gender,
                Description = userDomian.Description,
                Address = userDomian.Address,
            };
            return Ok(userDto);
        }
        [HttpPost]
        [Route("CreateRequest")]
        public async Task<IActionResult> CreateRequest([FromBody] AddRequestDto addRequestDto)
        {
            var requestDomainModel = new Request
            {
                InsurancePlan_Id = addRequestDto.InsurancePlan_Id,
                User_Id = addRequestDto.User_Id,
                Health_Id = addRequestDto.Health_Id,
                DocUrl  = addRequestDto.DocUrl,
            };
            var request = await requestRepository.CreateAsync(requestDomainModel);
            return Ok(request);
        }
        [HttpGet]
        [Route("getallrequests/{id:Guid}")]
        public async Task<IActionResult> GetAllRequests([FromRoute] Guid id)
        {
            var lists=await requestRepository.GetAllAsync();
            if(lists==null)
            {
                return NotFound();
            }
            var list = new List<Request>();
            foreach (Request x in lists)
            {
                if(x.User_Id==id)
                {
                    list.Add(x);
                }
            }
            return Ok(list);
        }
        [HttpGet]
        [Route("getallinvoice/{id:Guid}")]
        public async Task<IActionResult>GetAllInvoice([FromRoute] Guid id)
        {
            var list1 = await invoiceRepository.GetAllAsync();
            var list2 = await requestRepository.GetAllAsync();
            if (list1==null || list2==null)
            {
                return NotFound();
            }
            var list = new List<Request>();
            foreach (Request x in list2)
            {
                if (x.User_Id == id)
                {
                    list.Add(x);
                }
            }
            var list3 = new List<Invoice>();
            foreach (Request x in list)
            {
                var t = await dbContext.Invoices.FirstOrDefaultAsync(x1=>x1.Request_Id==x.Request_Id);
                if (t!=null)
                {
                    list3.Add(t);
                }
            }
            return Ok(list3);
        }
        [HttpPut]
        [Route("invoicepayment/{id:Guid}")]
        public async Task<IActionResult> UpdateInvoice([FromRoute] Guid id,[FromBody] UpdateUserInvoiceDto updateUserInvoiceDto)
        {
            var lists= await invoiceRepository.GetAllAsync();
            var invoice = new Invoice
            {
                status = 0,
            };
            invoice.status = 0;
            foreach (Invoice x in lists)
            {
                if (x.Invoice_Id == updateUserInvoiceDto.Invoice_Id)
                {
                    await invoiceRepository.UpdateAsync(x.Invoice_Id, invoice);
                    break;
                }
            }
            return Ok(lists);
        }
        [HttpPost]
        [Route("submitclaim")]
        public async Task<IActionResult> CreateClaim([FromBody] AddClaimDto addClaimDto)
        {
            var claimModel = new Claim
            {
                User_Id = addClaimDto.User_Id,
                Invoice_Id = addClaimDto.Invoice_Id,
                InsuranceCompant_Id = addClaimDto.InsuranceCompant_Id,
                User_Name = addClaimDto.User_Name,
                Dob = addClaimDto.Dob,
                Address = addClaimDto.Address,
                DateOfService = addClaimDto.DateOfService,
                Treatment = addClaimDto.Treatment,
                Diagnosis = addClaimDto.Diagnosis,
                Claim_Amount = addClaimDto.Claim_Amount,
                Invoice_Amount = addClaimDto.Invoice_Amount,
            };
            var claim= await claimRepository.CreateAsync(claimModel);
            return Ok(claim);
        }
        [HttpGet]
        [Route("getallclaims/{id:Guid}")]
        public async Task<IActionResult> GetAllClaim([FromRoute] Guid id)
        {
            var lists1= await claimRepository.GetAllAsync();
            if(lists1==null)
            {
                return NotFound();
            }
            var lists2 = new List<Claim>();
            foreach(Claim x in lists1)
            {
                if(x.User_Id == id)
                {
                    lists2.Add(x);
                }
            }
            return Ok(lists2);
        }
    }
}