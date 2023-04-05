using System.Runtime.CompilerServices;
using GenomeAnalyzer.DAL.Interfaces;
using GenomeAnalyzer.DAL.Repositories;
using GenomeAnalyzer.Domain.Entities;
using GenomeAnalyzer.Domain.Enum;
using GenomeAnalyzer.Domain.Response;
using GenomeAnalyzer.Domain.ViewModels;
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

    public async Task<IBaseResponse<GenomeEntity>> GetGenome(long id)
    {
        var entity = await _genomeRepository.GetAll().FirstOrDefaultAsync(e => e.Id == id);

        if (entity != null)
        {
            return new BaseResponse<GenomeEntity>()
            {
                Description = "Genome entity was found.",
                StatusCode = StatusCode.Ok,
                Data = entity
            };
        }

        return new BaseResponse<GenomeEntity>()
        {
            Description = "Cannot find genome.",
            StatusCode = StatusCode.InternalServerError
        };
    }

    public IQueryable<GenomeEntity> GetAll()
    {
        return _genomeRepository.GetAll();
    }

    public async Task<IBaseResponse<GenomeEntity>> Create(CreateGenomeViewModel model)
    {
        if (model.Name is not null && model.RawGenome is not null)
        {
            var entity = new GenomeEntity(model.Name, model.Type, model.RawGenome);
            
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