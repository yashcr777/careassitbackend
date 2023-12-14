using CareAssit.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CareAssit.Data
{
    public class CareAssitAuthDbContext : IdentityDbContext
    {
        public CareAssitAuthDbContext(DbContextOptions<CareAssitAuthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var userRoleId = "754522df-f533-4381-8515-4e20493ce2b5";
            var adminRoleId = "96aed04b-05d4-4449-be61-b6c56b129747";
            var healthProviderRoleId = "4d641ee6-bba9-4c5f-9ff8-2c6525ba4166";
            var insuranceCompanyRoleId = "d0702f26-8d85-4ce4-a708-6ac919c6bd53";
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id=userRoleId,
                    ConcurrencyStamp=userRoleId,
                    Name="User",
                    NormalizedName="User".ToUpper(),
                },
                new IdentityRole
                {
                    Id= adminRoleId,
                    ConcurrencyStamp= adminRoleId,
                    Name="Admin",
                    NormalizedName="Admin".ToUpper(),
                },
                new IdentityRole
                {
                    Id=healthProviderRoleId,
                    ConcurrencyStamp=healthProviderRoleId,
                    Name="HealthProvider",
                    NormalizedName="HealthProvider".ToUpper(),
                },
                new IdentityRole
                {
                    Id=insuranceCompanyRoleId,
                    ConcurrencyStamp=insuranceCompanyRoleId,
                    Name="InsurnaceCompany",
                    NormalizedName="InsuranceCompany".ToUpper(),
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);

        }
    }
}
