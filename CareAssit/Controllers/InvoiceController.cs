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
    public class InvoiceController : ControllerBase
    {
        private readonly CareAssitDbContext dbContext;
        private readonly IInvoiceRepository invoiceRepository;
        private readonly IMapper mapper;
        private readonly IClaimRepository claimRepository;

        public InvoiceController(CareAssitDbContext dbContext,IInvoiceRepository invoiceRepository,IMapper mapper,IClaimRepository claimRepository)
        {
            this.dbContext = dbContext;
            this.invoiceRepository = invoiceRepository;
            this.mapper = mapper;
            this.claimRepository = claimRepository;
        }
        [HttpGet]

        public async Task<IActionResult> GetAllRequest()
        {
            var invoiceDomainModel = await invoiceRepository.GetAllAsync();
            //Map Domain Model To Dto
            return Ok(mapper.Map<List<InvoiceDto>>(invoiceDomainModel));
        }
        [HttpGet]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "User")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var invoiceDomain = await invoiceRepository.GetByIdAsync(id);
            if (invoiceDomain == null)
            {
                return NotFound();
            }
            var invoiceDto = new InvoiceDto
            {
                Invoice_Id=invoiceDomain.Invoice_Id,
                Request_Id=invoiceDomain.Request_Id,
                InvoiceNumber=invoiceDomain.InvoiceNumber,
                InvoiceDate=invoiceDomain.InvoiceDate,
                DueDate=invoiceDomain.DueDate,
                Consultation_Fee=invoiceDomain.Consultation_Fee,
                Diag_Scan_Fee=invoiceDomain.Diag_Scan_Fee,
                Diag_Tests_Fee=invoiceDomain.Diag_Tests_Fee,
                Presc_Medication=invoiceDomain.Presc_Medication,
                Tax=invoiceDomain.Tax,
                Total_Amount=invoiceDomain.Total_Amount,
            };
            return Ok(invoiceDto);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddInvoiceDto addInvoiceDto)
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

            var invoiceDto = new InvoiceDto
            {
                Invoice_Id = invoiceModel.Invoice_Id,
                Request_Id = invoiceModel.Request_Id,
                InvoiceNumber = invoiceModel.InvoiceNumber,
                InvoiceDate = invoiceModel.InvoiceDate,
                DueDate = invoiceModel.DueDate,
                Consultation_Fee = invoiceModel.Consultation_Fee,
                Diag_Scan_Fee = invoiceModel.Diag_Scan_Fee,
                Diag_Tests_Fee = invoiceModel.Diag_Tests_Fee,
                Presc_Medication = invoiceModel.Presc_Medication,
                Tax = invoiceModel.Tax,
                Total_Amount = invoiceModel.Total_Amount,
            };
            return CreatedAtAction(nameof(GetById), new { id = invoiceDto.Invoice_Id }, invoiceDto);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var invoiceModel = await invoiceRepository.DeleteAsync(id);
            if (invoiceModel == null)
            {
                return NotFound();
            }
            var invoiceDto = new InvoiceDto
            {
                Invoice_Id = invoiceModel.Invoice_Id,
                Request_Id = invoiceModel.Request_Id,
                InvoiceNumber = invoiceModel.InvoiceNumber,
                InvoiceDate = invoiceModel.InvoiceDate,
                DueDate = invoiceModel.DueDate,
                Consultation_Fee = invoiceModel.Consultation_Fee,
                Diag_Scan_Fee = invoiceModel.Diag_Scan_Fee,
                Diag_Tests_Fee = invoiceModel.Diag_Tests_Fee,
                Presc_Medication = invoiceModel.Presc_Medication,
                Tax = invoiceModel.Tax,
                Total_Amount = invoiceModel.Total_Amount,
            };
            return Ok(invoiceDto);
        }
        /*[HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, Update updateInsurancePlanDto)
        {
            var insurancePlanDomainModel = mapper.Map<InsurancePlan>(updateInsurancePlanDto);
            insurancePlanDomainModel = await insurancePlanRepository.UpdateAsync(id, insurancePlanDomainModel);
            if (insurancePlanDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<InsurancePlanDto>(insurancePlanDomainModel));
        }*/
        [HttpGet]
        [Route("checkforclaim/{id:Guid}")]
        public async Task<IActionResult> CheckForClaim([FromRoute] Guid id)
        {
            var claims=await claimRepository.GetAllAsync();
            if(claims == null)
            {
                return NotFound();
            }
            foreach(Claim x in claims)
            {
                if(x.Invoice_Id== id) 
                {
                    return Ok(0);
                }
            }
            return NotFound();
        }
    }
}
