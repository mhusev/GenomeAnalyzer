using GenomeAnalyzer.DAL.Interfaces;
using GenomeAnalyzer.Domain.DTO;
using GenomeAnalyzer.Domain.Entities;
using GenomeAnalyzer.Domain.Enum;
using GenomeAnalyzer.Domain.Response;
using GenomeAnalyzer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GenomeAnalyzer.Services.Implementations;

public class GenomeService : IGenomeService
{
    private readonly IBaseRepository<GenomeEntity> _genomeRepository;

    public GenomeService(IBaseRepository<GenomeEntity> genomeRepository)
    {
        _genomeRepository = genomeRepository;
    }
    
    public async Task<IBaseResponse<GenomeEntity>> Get(long id)
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

    public async Task<IBaseResponse<GenomeEntity>> Edit(GenomeEntity entity)
    {
        if (entity != null)
        {
            var dbEntity = await _genomeRepository.Update(entity);

            if (dbEntity == entity)
            {
                return new BaseResponse<GenomeEntity>()
                {
                    Description = "Genome was updated successfully.",
                    StatusCode = StatusCode.Ok,
                    Data = dbEntity
                };
            }
        }
        
        return new BaseResponse<GenomeEntity>()
        {
            Description = "Something went wrong. Cannot update genome.",
            StatusCode = StatusCode.InternalServerError
        };
    }
    
    public async Task<IBaseResponse<GenomeEntity>> Create(CreateGenomeDTO dto)
    {
        if (dto is not null)
        {
            var entity = new GenomeEntity()
            {
                Name = dto.Name,
                Type = dto.Type,
                RawGenome = dto.RawGenome
            };
            
            await _genomeRepository.Create(entity);

            return new BaseResponse<GenomeEntity>()
            {
                Description = "Genome was added successfully.",
                StatusCode = StatusCode.Ok
            };
        }

        return new BaseResponse<GenomeEntity>()
        {
            Description = "Something went wrong. Cannot add genome.",
            StatusCode = StatusCode.InternalServerError
        };
    }

    public async Task<IBaseResponse<object>> Delete(long id)
    {
        var entity = await _genomeRepository.GetAll().FirstOrDefaultAsync(e => e.Id == id);

        if (entity != null)
        {
            await _genomeRepository.Delete(entity);

            return new BaseResponse<object>()
            {
                Description = "Genome was deleted successfully.",
                StatusCode = StatusCode.Ok
            };
        }

        return new BaseResponse<object>()
        {
            Description = "Something went wrong, cannot delete genome.",
            StatusCode = StatusCode.InternalServerError
        };
    }
}