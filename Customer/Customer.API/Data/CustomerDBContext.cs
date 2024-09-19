using Customer.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Customer.API.Data
{
    public class CustomerDBContext: DbContext
    {
        public CustomerDBContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
                
        }
        public DbSet<CustomerDB> Customers { get; set; }
    }
}
