using AutoMapper;
using CareAssit.Data;
using CareAssit.Models.Domain;
using CareAssit.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace CareAssit.Repositories
{
    public class SQLInvoiceRepository : IInvoiceRepository
    {
        private readonly CareAssitDbContext dbContext;

        public SQLInvoiceRepository(CareAssitDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Invoice> CreateAsync(Invoice invoice)
        {
            await dbContext.Invoices.AddAsync(invoice);
            await dbContext.SaveChangesAsync();
            return invoice;
        }

        public async Task<Invoice?> DeleteAsync(Guid id)
        {
            var existingInvoice = await dbContext.Invoices.FirstOrDefaultAsync(x => x.Invoice_Id == id);
            if (existingInvoice == null)
            {
                return null;
            }
            dbContext.Invoices.Remove(existingInvoice);
            await dbContext.SaveChangesAsync();
            return existingInvoice;
        }

        public async Task<List<Invoice>> GetAllAsync()
        {
            return await dbContext.Invoices.ToListAsync();
        }

        public async Task<Invoice?> GetByIdAsync(Guid id)
        {
            return await dbContext.Invoices.FirstOrDefaultAsync(x => x.Invoice_Id == id);
        }

        public async Task<Invoice?> UpdateAsync(Guid id, Invoice invoice)
        {
            var existingInvoice = await dbContext.Invoices.FirstOrDefaultAsync(x => x.Invoice_Id == id);
            if (existingInvoice == null)
            {
                return null;
            }
            /*existingInvoice.InvoiceDate=invoice.InvoiceDate;
            existingInvoice.DueDate=invoice.DueDate;
            existingInvoice.Presc_Medication=invoice.Presc_Medication;
            existingInvoice.InvoiceNumber=invoice.InvoiceNumber;
            existingInvoice.Consultation_Fee = invoice.Consultation_Fee;
            existingInvoice.Diag_Tests_Fee=invoice.Diag_Tests_Fee;
            existingInvoice.Diag_Scan_Fee=invoice.Diag_Scan_Fee;
            existingInvoice.Tax=invoice.Tax;
            existingInvoice.Total_Amount=invoice.Total_Amount;
            existingInvoice.Payment = invoice.Payment;*/
            existingInvoice.status = 0;
            await dbContext.SaveChangesAsync();
            return existingInvoice;
        }
    }
}
