using System.Runtime.CompilerServices;
using GenomeAnalyzer.DAL.Repositories;
using GenomeAnalyzer.Domain.Entities;
using GenomeAnalyzer.Domain.Enum;
using GenomeAnalyzer.Domain.Response;
using GenomeAnalyzer.Services.Interfaces;

namespace GenomeAnalyzer.Services.Implementations;

public class HomeService : IHomeService
{
    private readonly GenomeRepository _genomeRepository;

    public HomeService(GenomeRepository genomeRepository)
    {
        _genomeRepository = genomeRepository;
    }

    public IQueryable<GenomeEntity> GetAll()
    {
        return _genomeRepository.GetAll();
    }

    public async Task<IBaseResponse<GenomeEntity>> Create(GenomeEntity entity)
    {
        if (entity.Name is not null && entity.RawGenome is not null)
        {
            await _genomeRepository.Create(entity);

            return new BaseResponse<GenomeEntity>()
            {
                Description = "Genome was added successfully.",
                StatusCode = StatusCode.Ok
            };
        }

        return new BaseResponse<GenomeEntity>()
        {
            Description = "Cannot add genome.",
            StatusCode = StatusCode.InternalServerError
        };
    }
}