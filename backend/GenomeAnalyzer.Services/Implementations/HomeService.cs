using GenomeAnalyzer.DAL.Interfaces;
using GenomeAnalyzer.Domain.Distribution;
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
            Description = "Something went wrong.",
            StatusCode = StatusCode.InternalServerError
        };
    }

    public IQueryable<GenomeEntity> GetAll()
    {
        return _genomeRepository.GetAll();
    }

    public async Task<IBaseResponse<GenomeEntity>> Create(CreateGenomeViewModel model)
    {
        if (model is not null)
        {
            var entity = new GenomeEntity()
            {
                Name = model.Name,
                Type = model.Type,
                RawGenome = model.RawGenome
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
            Description = "Cannot add genome.",
            StatusCode = StatusCode.InternalServerError
        };
    }

    public async Task<IBaseResponse<bool>> Delete(long id)
    {
        var entity = await _genomeRepository.GetAll().FirstOrDefaultAsync(e => e.Id == id);

        if (entity != null)
        {
            await _genomeRepository.Delete(entity);

            return new BaseResponse<bool>()
            {
                Description = "Genome was deleted successfully.",
                StatusCode = StatusCode.Ok,
                Data = true
            };
        }

        return new BaseResponse<bool>()
        {
            Description = "Something went wrong, cannot delete genome.",
            StatusCode = StatusCode.InternalServerError,
            Data = false
        };
    }

    public async Task<IBaseResponse<GenomeEntity>> Distribute(long id)
    {
        var entity = await _genomeRepository.GetAll().FirstOrDefaultAsync(e => e.Id == id);

        if (entity != null)
        {
            return new BaseResponse<GenomeEntity>()
            {
                Description = "Undistributed genome was given successfully.",
                StatusCode = StatusCode.Ok,
                Data = entity
            };
        }
        
        return new BaseResponse<GenomeEntity>()
        {
            Description = "Something went wrong, cannot get a genome.",
            StatusCode = StatusCode.InternalServerError,
        };
    }

    public async Task<IBaseResponse<DistributionData>> GetDistributionData(DistributionParams distributionParams)
    {
        var entity = await _genomeRepository.GetAll().FirstOrDefaultAsync(e => e.Id == distributionParams.Id);

        if (entity == null)
        {
            return new BaseResponse<DistributionData>()
            {
                Description = "Genome's cannot be obtained.",
                StatusCode = StatusCode.InternalServerError
            };
        }
        
        if (distributionParams.Nucleotide != null)
        {
            return new BaseResponse<DistributionData>()
            {
                Description = "Genome was distributed successfully.",
                StatusCode = StatusCode.Ok,
                Data = distributionParams.Nucleotide switch
                {
                    'a' => DistributionHelper.DistributeGenomeByAdenine(entity.RawGenome),
                    'c' => DistributionHelper.DistributeGenomeByCytosine(entity.RawGenome),
                    'g' => DistributionHelper.DistributeGenomeByGuanine(entity.RawGenome),
                    't' => DistributionHelper.DistributeGenomeByThymine(entity.RawGenome),
                    _   => throw new ArgumentOutOfRangeException(nameof(distributionParams.Nucleotide),
                        $"Not expected nucleotide value: {distributionParams.Nucleotide}")  
                 }
            };
        }

        if (distributionParams.SequenceLength != null && distributionParams.StartPosition != null)
        {
            return new BaseResponse<DistributionData>()
            {
                Description = "Genome was distributed successfully.",
                StatusCode = StatusCode.Ok,
                Data = DistributionHelper.DistributeGenomeByConstantLength(entity.RawGenome, 
                    (int)distributionParams.SequenceLength, 
                    (int)distributionParams.StartPosition)
            };
        }

        if (distributionParams.SequenceLength != null && distributionParams.StartPosition == null)
        {
            return new BaseResponse<DistributionData>()
            {
                Description = "Genome was distributed successfully.",
                StatusCode = StatusCode.Ok,
                Data = DistributionHelper.DistributeGenomeByNgram(entity.RawGenome, 
                    (int)distributionParams.SequenceLength)
            };
        }

        return new BaseResponse<DistributionData>()
        {
            Description = "There are not enough parameters to distribute.",
            StatusCode = StatusCode.InternalServerError
        };
    }
}