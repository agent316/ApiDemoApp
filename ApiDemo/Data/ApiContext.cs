using Microsoft.EntityFrameworkCore;
using ApiDemo.Models;

namespace ApiDemo.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
            
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Reports> Reports { get; set; }
    }
}
