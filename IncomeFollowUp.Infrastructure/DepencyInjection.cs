using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IncomeFollowUp.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<IncomeFollowUpContext>(options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));
    }
}