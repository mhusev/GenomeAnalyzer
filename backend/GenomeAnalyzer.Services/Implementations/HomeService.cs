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