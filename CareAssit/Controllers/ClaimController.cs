using AutoMapper;
using CareAssit.Data;
using CareAssit.Models.Domain;
using CareAssit.Models.DTO;
using CareAssit.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareAssit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimController : ControllerBase
    {
        private readonly CareAssitDbContext dbContext;
        private readonly IClaimRepository claimRepository;
        private readonly IMapper mapper;

        public ClaimController(CareAssitDbContext dbContext, IClaimRepository claimRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.claimRepository = claimRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        //[Authorize(Roles ="User")]
        public async Task<IActionResult> GetAllRequest()
        {
            var claimDomainModel = await claimRepository.GetAllAsync();
            //Map Domain Model To Dto
            return Ok(mapper.Map<List<ClaimDto>>(claimDomainModel));
        }
        [HttpGet]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "User")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var claimDomainModel = await claimRepository.GetByIdAsync(id);
            if (claimDomainModel == null)
            {
                return NotFound();
            }
            var claimDto = new ClaimDto
            {
                Claim_Id = claimDomainModel.Claim_Id,
                User_Id = claimDomainModel.User_Id,
                Invoice_Id = claimDomainModel.Invoice_Id,
                InsuranceCompant_Id = claimDomainModel.InsuranceCompant_Id,
                User_Name = claimDomainModel.User_Name,
                Dob = claimDomainModel.Dob,
                Address = claimDomainModel.Address,
                DateOfService = claimDomainModel.DateOfService,
                Treatment = claimDomainModel.Treatment,
                Diagnosis = claimDomainModel.Diagnosis,
                Claim_Amount = claimDomainModel.Claim_Amount,
                Invoice_Amount = claimDomainModel.Invoice_Amount
            };
            return Ok(claimDto);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddClaimDto addClaimDto)
        {
            var claimDomainModel = new Claim
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
                Invoice_Amount = addClaimDto.Invoice_Amount
            };
            claimDomainModel = await claimRepository.CreateAsync(claimDomainModel);
            var claimDto = new ClaimDto
            {
                Claim_Id = claimDomainModel.Claim_Id,
                User_Id = claimDomainModel.User_Id,
                Invoice_Id = claimDomainModel.Invoice_Id,
                InsuranceCompant_Id = claimDomainModel.InsuranceCompant_Id,
                User_Name = claimDomainModel.User_Name,
                Dob = claimDomainModel.Dob,
                Address = claimDomainModel.Address,
                DateOfService = claimDomainModel.DateOfService,
                Treatment = claimDomainModel.Treatment,
                Diagnosis = claimDomainModel.Diagnosis,
                Claim_Amount = claimDomainModel.Claim_Amount,
                Invoice_Amount = claimDomainModel.Invoice_Amount
            };
            return CreatedAtAction(nameof(GetById), new { id = claimDto.Claim_Id }, claimDto);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var claimDomainModel = await claimRepository.DeleteAsync(id);
            if (claimDomainModel == null)
            {
                return NotFound();
            }
            var claimDto = new ClaimDto
            {
                Claim_Id = claimDomainModel.Claim_Id,
                User_Id = claimDomainModel.User_Id,
                Invoice_Id = claimDomainModel.Invoice_Id,
                InsuranceCompant_Id = claimDomainModel.InsuranceCompant_Id,
                User_Name = claimDomainModel.User_Name,
                Dob = claimDomainModel.Dob,
                Address = claimDomainModel.Address,
                DateOfService = claimDomainModel.DateOfService,
                Treatment = claimDomainModel.Treatment,
                Diagnosis = claimDomainModel.Diagnosis,
                Claim_Amount = claimDomainModel.Claim_Amount,
                Invoice_Amount = claimDomainModel.Invoice_Amount
            };
            return Ok(claimDto);
        }
        /*[HttpPut]
        [Route("updateclaim/{id:Guid}")]
        public async Task<IActionResult> Updateclaim([FromRoute] Guid id, [FromBody] ClaimDto claimDto)
        {
            var claimmodel = new Claim
            {
                Claim_Id = claimDto.Claim_Id,
                User_Id = claimDto.User_Id,
                Invoice_Id = claimDto.Invoice_Id,
                InsuranceCompant_Id = claimDto.InsuranceCompant_Id,
                User_Name = claimDto.User_Name,
                Dob = claimDto.Dob,
                Address = claimDto.Address,
                DateOfService = claimDto.DateOfService,
                Treatment = claimDto.Treatment,
                Diagnosis = claimDto.Diagnosis,
                Claim_Amount = claimDto.Claim_Amount,
                Invoice_Amount = claimDto.Invoice_Amount,
                status2 = 0,
            };
            claimmodel = await claimRepository.UpdateAsync(id, claimmodel);
            if (claimmodel == null)
            {
                return NotFound();
            }
            return Ok(claimmodel);
        }*/
    }
}
