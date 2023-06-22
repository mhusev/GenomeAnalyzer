using GenomeAnalyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GenomeAnalyzer.DAL;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
        
    public DbSet<GenomeEntity> Genomes { get; set; }
}