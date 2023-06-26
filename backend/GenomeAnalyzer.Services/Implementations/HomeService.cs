using GenomeAnalyzer.DAL.Interfaces;
using GenomeAnalyzer.Domain.Distribution;
using GenomeAnalyzer.Domain.Entities;
using GenomeAnalyzer.Domain.Enum;
using GenomeAnalyzer.Domain.Response;
using GenomeAnalyzer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GenomeAnalyzer.Services.Implementations;

public class HomeService : IHomeService
{
    private readonly IBaseRepository<GenomeEntity> _genomeRepository;

    public HomeService(IBaseRepository<GenomeEntity> genomeRepository)
    {
        _genomeRepository = genomeRepository;
    }

    public IQueryable<GenomeEntity> GetAll()
    {
        return _genomeRepository.GetAll();
    }
}