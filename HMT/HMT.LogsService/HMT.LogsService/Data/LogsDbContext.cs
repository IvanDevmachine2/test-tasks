using HMT.LogsService.Models;
using Microsoft.EntityFrameworkCore;

namespace HMT.LogsService.Data
{
    public class LogsDbContext(DbContextOptions<LogsDbContext> options) : DbContext(options)
    {
        public DbSet<LogEntry> LogEntries { get; set; }
    }
}
