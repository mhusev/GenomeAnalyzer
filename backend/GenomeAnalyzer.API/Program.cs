using GenomeAnalyzer.DAL;
using GenomeAnalyzer.DAL.Interfaces;
using GenomeAnalyzer.DAL.Repositories;
using GenomeAnalyzer.Domain.Entities;
using GenomeAnalyzer.Services.Implementations;
using GenomeAnalyzer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IBaseRepository<GenomeEntity>, GenomeRepository>();
builder.Services.AddTransient<IHomeService, HomeService>();
builder.Services.AddTransient<IGenomeService, GenomeService>();
builder.Services.AddTransient<IDistributionService, DistributionService>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL"));
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id}");

app.Run();