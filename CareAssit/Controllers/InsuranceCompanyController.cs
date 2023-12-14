using AutoMapper;
using CareAssit.Data;
using CareAssit.Models.Domain;
using CareAssit.Models.DTO;
using CareAssit.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CareAssit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceCompanyController : ControllerBase
    {
        private readonly CareAssitAuthDbContext dbContext;
        private readonly IInsuranceCompanyRepository insuranceCompanyRepository;
        private readonly IMapper mapper;
        private readonly IClaimRepository claimRepository;

        public InsuranceCompanyController(CareAssitAuthDbContext dbContext, IInsuranceCompanyRepository insuranceCompanyRepository,IMapper mapper,IClaimRepository claimRepository)
        {
            this.dbContext = dbContext;
            this.insuranceCompanyRepository = insuranceCompanyRepository;
            this.mapper = mapper;
            this.claimRepository = claimRepository;
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var insuranceCompanyModel = await insuranceCompanyRepository.GetByIdAsync(id);
            if (insuranceCompanyModel == null)
            {
                return NotFound();
            }
            var insuranceCompanyDto = new InsuranceCompanyDto
            {
                InsuranceCompany_Id=insuranceCompanyModel.InsuranceCompany_Id,
                InsuranceCompany_Name = insuranceCompanyModel.InsuranceCompany_Name,
                InsuranceCompany_Description = insuranceCompanyModel.InsuranceCompany_Description
            };
            return Ok(insuranceCompanyDto);
        }
        [HttpGet]
        //[Authorize(Roles ="User")]
        public async Task<IActionResult> GetAll()
        {
            var insuranceCompanyDomain = await insuranceCompanyRepository.GetAllAsync();
            //Map Domain Model To Dto
            return Ok(mapper.Map<List<InsuranceCompanyDto>>(insuranceCompanyDomain));
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddInsuranceCompanyDto addInsuranceCompanyDto)
        {
            var insuranceCompanyModel = new InsuranceCompany
            {
                Email= addInsuranceCompanyDto.Email,
                Password= addInsuranceCompanyDto.Password,
                InsuranceCompany_Name = addInsuranceCompanyDto.InsuranceCompany_Name,
                InsuranceCompany_Description = addInsuranceCompanyDto.InsuranceCompany_Description,
            };
            insuranceCompanyModel = await insuranceCompanyRepository.CreateAsync(insuranceCompanyModel);
            var insuranceCompanyDto = new InsuranceCompanyDto
            {
                InsuranceCompany_Id = insuranceCompanyModel.InsuranceCompany_Id,
                Email=insuranceCompanyModel.Email,
                Password=insuranceCompanyModel.Password,
                InsuranceCompany_Name = insuranceCompanyModel.InsuranceCompany_Name,
                InsuranceCompany_Description = insuranceCompanyModel.InsuranceCompany_Description
            };
            return CreatedAtAction(nameof(GetById), new { id = insuranceCompanyDto.InsuranceCompany_Id }, insuranceCompanyDto);
        }
        [HttpDelete]
        //[Authorize(Roles = "HealthProvider")]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var insuranceComDomain = await insuranceCompanyRepository.DeleteAsync(id);
            if (insuranceComDomain == null)
            {
                return NotFound();
            }
            var insuranceComDto = new InsuranceCompanyDto
            {
                InsuranceCompany_Id=insuranceComDomain.InsuranceCompany_Id,
                InsuranceCompany_Description=insuranceComDomain.InsuranceCompany_Description,
                InsuranceCompany_Name=insuranceComDomain.InsuranceCompany_Name,
            };
            return Ok(insuranceComDto);
        }
        [HttpPut]
        [Route("acceptclaim/{id:Guid}")]
        public async Task<IActionResult> ApproveClaim([FromRoute] Guid id,[FromBody] UpdateClaimDto updateClaimDto)
        {
            var lists=await claimRepository.GetAllAsync();
            var claim = new Claim
            {
                status2 = 0,
            };
            claim.status2 = 0;
            foreach(Claim x in lists)
            {
                if(x.Claim_Id==updateClaimDto.Claim_Id)
                {
                    await claimRepository.UpdateAsync(x.Claim_Id,claim);
                    break;
                }
            }
            return Ok(lists);
        }
        [HttpGet]
        [Route("getallclaims/{id:Guid}")]
        public async Task<IActionResult> GetAllClaim([FromRoute] Guid id)
        {
            var lists1 = await claimRepository.GetAllAsync();
            if (lists1 == null)
            {
                return NotFound();
            }
            var lists2 = new List<Claim>();
            foreach (Claim x in lists1)
            {
                if (x.InsuranceCompant_Id == id)
                {
                    lists2.Add(x);
                }
            }
            return Ok(lists2);
        }
    }
}
