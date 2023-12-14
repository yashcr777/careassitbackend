using CareAssit.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CareAssit.Data
{
    public class CareAssitDbContext : DbContext
    {
        public CareAssitDbContext(DbContextOptions<CareAssitDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<HealthProvider> HealthProviders { get; set; }
        public DbSet<InsurancePlan> InsurancePlans { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<InsuranceCompany> InsuranceCompanies { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<SignUp> SignUps { get; set; }
        //public DbSet<RequestForClaim> RequestsForClaims { get; set; }
    }
}
