using GenomeAnalyzer.DAL.Interfaces;
using GenomeAnalyzer.Domain.Distribution;
using GenomeAnalyzer.Domain.DTO;
using GenomeAnalyzer.Domain.Entities;
using GenomeAnalyzer.Domain.Enum;
using GenomeAnalyzer.Domain.Response;
using GenomeAnalyzer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GenomeAnalyzer.Services.Implementations;

public class DistributionService : IDistributionService
{
    private readonly IBaseRepository<GenomeEntity> _genomeRepository;

    public DistributionService(IBaseRepository<GenomeEntity> genomeRepository)
    {
        _genomeRepository = genomeRepository;
    }
    
    public async Task<IBaseResponse<DistributionData>> Distribute(DistributionParamsDTO dto)
    {
        var entity = await _genomeRepository.GetAll().FirstOrDefaultAsync(e => e.Id == dto.Id);

        if (entity == null)
        {
            return new BaseResponse<DistributionData>()
            {
                Description = "Genome's cannot be obtained.",
                StatusCode = StatusCode.InternalServerError
            };
        }
        
        if (dto.Nucleotide != null)
        {
            return new BaseResponse<DistributionData>()
            {
                Description = "Genome was distributed successfully.",
                StatusCode = StatusCode.Ok,
                Data = dto.Nucleotide switch
                {
                    'a' => DistributionHelper.DistributeGenomeByAdenine(entity.RawGenome),
                    'c' => DistributionHelper.DistributeGenomeByCytosine(entity.RawGenome),
                    'g' => DistributionHelper.DistributeGenomeByGuanine(entity.RawGenome),
                    't' => DistributionHelper.DistributeGenomeByThymine(entity.RawGenome),
                    _   => throw new ArgumentOutOfRangeException(nameof(dto.Nucleotide),
                        $"Not expected nucleotide value: {dto.Nucleotide}")  
                 }
            };
        }

        if (dto.SequenceLength != null && dto.StartPosition != null)
        {
            return new BaseResponse<DistributionData>()
            {
                Description = "Genome was distributed successfully.",
                StatusCode = StatusCode.Ok,
                Data = DistributionHelper.DistributeGenomeByConstantLength(entity.RawGenome, 
                    (int)dto.SequenceLength, 
                    (int)dto.StartPosition)
            };
        }

        if (dto.SequenceLength != null && dto.StartPosition == null)
        {
            return new BaseResponse<DistributionData>()
            {
                Description = "Genome was distributed successfully.",
                StatusCode = StatusCode.Ok,
                Data = DistributionHelper.DistributeGenomeByNgram(entity.RawGenome, 
                    (int)dto.SequenceLength)
            };
        }

        return new BaseResponse<DistributionData>()
        {
            Description = "There are not enough parameters to distribute.",
            StatusCode = StatusCode.InternalServerError
        };
    }
}