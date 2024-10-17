using IncomeFollowUp.Infrastructure;
using IncomeFollowUp.Application;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using IncomeFollowUp.Api.Common.Errors;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

builder.Services.AddSingleton<ProblemDetailsFactory, IncomeFollowUpProblemDetailsFactory>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseWebAssemblyDebugging();
}

app.UseExceptionHandler("/error");
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.MapControllers();
app.MapRazorPages();
app.MapFallbackToFile("index.html");

app.UseHttpsRedirection();
app.Run();