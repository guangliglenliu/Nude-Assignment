using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NudeAssignment.Models
{
    public class _CoverageDBContext : DbContext
    {
        public _CoverageDBContext(DbContextOptions<_CoverageDBContext> options)
            : base(options)
        {
        }

        public DbSet<LKPCoverageItemCategory> LKPCoverageItemCategories { get; set; }
        public DbSet<CoverageItem> CoverageItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerItem> CustomerItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LKPCoverageItemCategory>().HasData(
                new LKPCoverageItemCategory() { CategoryId = 1, Name = "Clothing", Description = "Cloth items, jacket etc." },
                new LKPCoverageItemCategory() { CategoryId = 2, Name = "Electroics", Description = "TV, Radio, Playstation..." },
                new LKPCoverageItemCategory() { CategoryId = 3, Name = "kitchen", Description = "Pots, Flatware..." });
        }
    }
}
