using IncomeFollowUp.Domain;
using Microsoft.EntityFrameworkCore;

namespace IncomeFollowUp.Infrastructure;

public class IncomeFollowUpContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Settings> Settings { get; set; }
    public DbSet<WorkDay> WorkDays { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
