using AutoMapper;
using CareAssit.Models.Domain;
using CareAssit.Models.DTO;
using CareAssit.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareAssit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsurancePlanController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IInsurancePlanRepository insurancePlanRepository;


        public InsurancePlanController(IMapper mapper, IInsurancePlanRepository insurancePlanRepository)
        {
            this.mapper = mapper;
            this.insurancePlanRepository = insurancePlanRepository;
        }

        //create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddInsurancePlanDto addInsurancePlanDto)
        {
            //var insurancePlanDomainModel = mapper.Map<InsurancePlan>(addInsurancePlanDto);
            //await insurancePlanRepository.CreateAsync(insurancePlanDomainModel);
            //return Ok(mapper.Map<InsurancePlanDto>(insurancePlanDomainModel));


            var insuranceDomainModel = new InsurancePlan
            {
                InsuranceCompany_Id = addInsurancePlanDto.InsuranceCompany_Id,
                Insurance_Name =addInsurancePlanDto.Insurance_Name,
                Insurance_Description=addInsurancePlanDto.Insurance_Description,
                Insurance_Price=addInsurancePlanDto.Insurance_Price,
                Insurance_Duration=addInsurancePlanDto.Insurance_Duration
            };
            insuranceDomainModel = await insurancePlanRepository.CreateAsync(insuranceDomainModel);

            var insuranceDto = new InsurancePlanDto
            {
                InsurancePlan_Id=insuranceDomainModel.InsurancePlan_Id,
                InsuranceCompany_Id=insuranceDomainModel.InsuranceCompany_Id,
                Insurance_Name=insuranceDomainModel.Insurance_Name,
                Insurance_Description=insuranceDomainModel.Insurance_Description,
                Insurance_Price=insuranceDomainModel.Insurance_Price,
                Insurance_Duration=insuranceDomainModel.Insurance_Duration,
            };
            return CreatedAtAction(nameof(GetById), new { id = insuranceDto.InsurancePlan_Id }, insuranceDto);
        }

        //Get All
        [HttpGet]
        public async Task<IActionResult>GetAll()
        {
            var insurancePlanDomainModel = await insurancePlanRepository.GetAllAsync();
            //Map Domain Model To Dto
            return Ok(mapper.Map<List<InsurancePlanDto>>(insurancePlanDomainModel));
        }

        //GetById

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var insurancePlanDomainModel=await insurancePlanRepository.GetByIdAsync(id);
            if(insurancePlanDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<InsurancePlanDto>(insurancePlanDomainModel));
        }

        //Update

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateInsurancePlanDto updateInsurancePlanDto)
        {
            var insurancePlanDomainModel = mapper.Map<InsurancePlan>(updateInsurancePlanDto);
            insurancePlanDomainModel = await insurancePlanRepository.UpdateAsync(id, insurancePlanDomainModel);
            if(insurancePlanDomainModel == null) 
            {
                return NotFound();
            }
            return Ok(mapper.Map<InsurancePlanDto>(insurancePlanDomainModel));
        }

        //Delete

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedInsurancePlanDomainModel=await insurancePlanRepository.DeleteAsync(id);
            if(deletedInsurancePlanDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<InsurancePlanDto>(deletedInsurancePlanDomainModel));
        }
    }
}