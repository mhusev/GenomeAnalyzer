using System.Linq;
using System.Threading.Tasks;
using GenomeAnalyzer.DAL.Interfaces;
using GenomeAnalyzer.Domain.Entities;

namespace GenomeAnalyzer.DAL.Repositories;

public class GenomeRepository : IBaseRepository<GenomeEntity>
{
    private readonly AppDbContext _appDbContext;

    public GenomeRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    public async Task Create(GenomeEntity entity)
    {
        await _appDbContext.Genomes.AddAsync(entity);
        await _appDbContext.SaveChangesAsync();
    }

    public IQueryable<GenomeEntity> GetAll()
    {
        return _appDbContext.Genomes;
    }

    public async Task Delete(GenomeEntity entity)
    {
        _appDbContext.Remove(entity);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<GenomeEntity> Update(GenomeEntity entity)
    {
        _appDbContext.Update(entity);
        await _appDbContext.SaveChangesAsync();

        return entity;
    }
}