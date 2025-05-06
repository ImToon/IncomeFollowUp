using System.Text.Json;
using IncomeFollowUp.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IncomeFollowUp.Infrastructure;

public static class DbSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IncomeFollowUpContext>();

        await db.Database.MigrateAsync();

        if (!db.MonthlyOutcomes.Any())
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Seed", "MonthlyOutcomes.json");
            if (File.Exists(path))
            {
                var json = await File.ReadAllTextAsync(path);
                var data = JsonSerializer.Deserialize<List<MonthlyOutcome>>(json);

                if (data is not null)
                {
                    db.MonthlyOutcomes.AddRange(data);
                    await db.SaveChangesAsync();
                }
            }
        }

        // Seed Settings if not present
        if (!db.Settings.Any())
        {

            var path = Path.Combine(AppContext.BaseDirectory, "Seed", "Settings.json");
            if (File.Exists(path))
            {
                var json = await File.ReadAllTextAsync(path);
                var data = JsonSerializer.Deserialize<Settings>(json);
                
                if (data is not null)
                {
                    db.Settings.Add(data);
                    await db.SaveChangesAsync();
                }
            }

            await db.SaveChangesAsync();
        }
    }
}
