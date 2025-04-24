using HMT_UserService.Models.DbModels;
using Microsoft.EntityFrameworkCore;

namespace HMT_UserService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
    }
}
