using AutoMapper;
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
    public class HealthProviderController : ControllerBase
    {
        private readonly CareAssitDbContext dbContext;
        private readonly IHealthProviderRepository healthProviderRepository;
        private readonly IRequestRepository requestRepository;
        private readonly IMapper mapper;
        private readonly IInvoiceRepository invoiceRepository;

        public HealthProviderController(CareAssitDbContext dbContext, IHealthProviderRepository healthProviderRepository, IRequestRepository requestRepository, IMapper mapper, IInvoiceRepository invoiceRepository)
        {
            this.dbContext = dbContext;
            this.healthProviderRepository = healthProviderRepository;
            this.requestRepository = requestRepository;
            this.mapper = mapper;
            this.invoiceRepository = invoiceRepository;
        }
        [HttpGet]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "User")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var healthProviderDomain = await healthProviderRepository.GetByIdAsync(id);
            if (healthProviderDomain == null)
            {
                return NotFound();
            }
            var healthProviderDto = new HealthProviderDto
            {
                Health_Id = healthProviderDomain.Health_Id,
                HelathProvider_Name = healthProviderDomain.HelathProvider_Name,
                CertificateNumber = healthProviderDomain.CertificateNumber,
            };
            return Ok(healthProviderDto);
        }
        [HttpPost]
        //[Authorize(Roles = "User,HealthProvider")]
        public async Task<IActionResult> Create([FromBody] AddHealthProviderDto addHealthProviderDto)
        {
            var healthProvierModel = new HealthProvider
            {
                HelathProvider_Name = addHealthProviderDto.HelathProvider_Name,
                CertificateNumber = addHealthProviderDto.CertificateNumber,
                Email=addHealthProviderDto.Email,
                Password=addHealthProviderDto.Password,
            };
            healthProvierModel = await healthProviderRepository.CreateAsync(healthProvierModel);

            var healthProviderDto = new HealthProviderDto
            {
                Health_Id = healthProvierModel.Health_Id,
                HelathProvider_Name = healthProvierModel.HelathProvider_Name,
                CertificateNumber = healthProvierModel.CertificateNumber,
                Email= healthProvierModel.Email,
                Password=healthProvierModel.Password,
            };
            return CreatedAtAction(nameof(GetById), new { id = healthProviderDto.Health_Id }, healthProviderDto);
        }
        [HttpDelete]
        //[Authorize(Roles = "HealthProvider")]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var healthProviderModel = await healthProviderRepository.DeleteAsync(id);
            if (healthProviderModel == null)
            {
                return NotFound();
            }
            var healthProviderDto = new HealthProviderDto
            {
                Health_Id = healthProviderModel.Health_Id,
                HelathProvider_Name = healthProviderModel.HelathProvider_Name,
                CertificateNumber = healthProviderModel.CertificateNumber,
            };
            return Ok(healthProviderDto);
        }

        [HttpGet]
        [Route("all")]
        //[Authorize(Roles ="User")]
        public async Task<IActionResult> GetAllRequest()
        {
            var healthProviderModel = await healthProviderRepository.GetAllAsync();
            //Map Domain Model To Dto
            return Ok(mapper.Map<List<HealthProviderDto>>(healthProviderModel));
        }



        [HttpGet]
        [Route("allrequest/{Health_Id:Guid}")]
        public async Task<IActionResult> GetAllReq(Guid Health_Id)
        {
            //Request singlerequest= await dbContext.Requests.FirstOrDefaultAsync(x=>x.Health_Id==id);
            var lists = await requestRepository.GetAllAsync();
            var list = new List<Request>();

            if (lists == null)
            {
                return NotFound();
            }
            foreach (Request x in lists)
            {
                if (x.Health_Id == Health_Id)
                {
                    list.Add(x);
                }
            }
            return Ok(list);
        }
        [HttpGet]
        [Route("getallinvoice/{id:Guid}")]
        public async Task<IActionResult> GetAllInv(Guid id)
        {
            var list1 = await invoiceRepository.GetAllAsync();
            var list2 = await requestRepository.GetAllAsync();
            if (list1 == null || list2 == null)
            {
                return NotFound();
            }
            var list = new List<Request>();
            foreach (Request x in list2)
            {
                if (x.Health_Id == id)
                {
                    list.Add(x);
                }
            }
            var list3 = new List<Invoice>();
            foreach (Request x in list)
            {
                var t = await dbContext.Invoices.FirstOrDefaultAsync(x1 => x1.Request_Id == x.Request_Id);
                if (t != null)
                {
                    list3.Add(t);
                }
            }
            return Ok(list3);
        }
        [HttpPost]
        [Route("createinvoice")]
        public async Task<IActionResult> CreateInvoice([FromBody] AddInvoiceDto addInvoiceDto)
        {
            var invoiceModel = new Invoice
            {
                Request_Id = addInvoiceDto.Request_Id,
                InvoiceNumber = addInvoiceDto.InvoiceNumber,
                InvoiceDate = addInvoiceDto.InvoiceDate,
                DueDate = addInvoiceDto.DueDate,
                Consultation_Fee = addInvoiceDto.Consultation_Fee,
                Diag_Scan_Fee = addInvoiceDto.Diag_Scan_Fee,
                Diag_Tests_Fee = addInvoiceDto.Diag_Tests_Fee,
                Presc_Medication = addInvoiceDto.Presc_Medication,
                Tax = addInvoiceDto.Tax,
                Total_Amount = addInvoiceDto.Total_Amount,
            };
            invoiceModel = await invoiceRepository.CreateAsync(invoiceModel);
            var lists = await requestRepository.GetAllAsync();
            foreach (Request x in lists)
            {
                if (x.Request_Id == addInvoiceDto.Request_Id)
                {
                    x.status1 = Models.Domain.Request.Status1.Submitted;
                    dbContext.SaveChanges();
                    break;
                }
            }
            return Ok(invoiceModel);
        }
    }
}
