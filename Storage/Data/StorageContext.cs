#nullable disable
using Microsoft.EntityFrameworkCore;
using Storage.Models.Entities;

namespace Storage.Data
{
    public class StorageContext : DbContext
    {
        public StorageContext (DbContextOptions<StorageContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; } 
    }
}
 