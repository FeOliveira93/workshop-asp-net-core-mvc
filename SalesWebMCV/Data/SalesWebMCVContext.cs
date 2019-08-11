
using Microsoft.EntityFrameworkCore;

namespace SalesWebMCV.Models
{
    public class SalesWebMCVContext : DbContext
    {
        public SalesWebMCVContext (DbContextOptions<SalesWebMCVContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Department { get; set; }
        public DbSet<Seller> Seller { get; set; }
        public DbSet<SalesRecord> SalesRecord { get; set; }
    }
}
