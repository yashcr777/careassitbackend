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
    public class RequestController : ControllerBase
    {
        private readonly IRequestRepository requestRepository;
        private readonly IMapper mapper;
        private readonly CareAssitDbContext dbContext;

        public RequestController(CareAssitDbContext dbContext, IRequestRepository requestRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.requestRepository = requestRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        //[Authorize(Roles ="User")]
        public async Task<IActionResult> GetAllRequest()
        {
            var requestDomainModel = await requestRepository.GetAllAsync();
            //Map Domain Model To Dto
            return Ok(mapper.Map<List<RequestDto>>(requestDomainModel));
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRequestDto addRequestDto)
        {
            var requestDomian = new Request
            {
                User_Id = addRequestDto.User_Id,
                InsurancePlan_Id = addRequestDto.InsurancePlan_Id,
                Health_Id = addRequestDto.Health_Id,
                DocUrl=addRequestDto.DocUrl,
            };
            requestDomian = await requestRepository.CreateAsync(requestDomian);
            

            var requestDto = new RequestDto
            {
                Request_Id = requestDomian.Request_Id,
                User_Id = requestDomian.User_Id,
                InsurancePlan_Id = requestDomian.InsurancePlan_Id,
                Health_Id = requestDomian.Health_Id,
                DocUrl= requestDomian.DocUrl,
            };
            return CreatedAtAction(nameof(GetById), new { id = requestDto.Request_Id }, requestDto);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "User")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var requestDomainModel = await requestRepository.GetByIdAsync(id);
            if (requestDomainModel == null)
            {
                return NotFound();
            }
            var requestDto = new RequestDto
            {
                Request_Id = requestDomainModel.Request_Id,
                InsurancePlan_Id = requestDomainModel.InsurancePlan_Id,
                User_Id = requestDomainModel.User_Id,
                Health_Id = requestDomainModel.Health_Id,
                DocUrl = requestDomainModel.DocUrl,
            };
            return Ok(requestDto);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var requestDomain = await requestRepository.DeleteAsync(id);
            if (requestDomain == null)
            {
                return NotFound();
            }
            var requestDto = new RequestDto
            {
                Request_Id = requestDomain.Request_Id,
                InsurancePlan_Id = requestDomain.InsurancePlan_Id,
                User_Id = requestDomain.User_Id,
                Health_Id = requestDomain.Health_Id,
                DocUrl=requestDomain.DocUrl,
            };
            return Ok(requestDto);
        }
    }
}
