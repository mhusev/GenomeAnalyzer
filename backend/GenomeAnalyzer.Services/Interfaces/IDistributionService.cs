using GenomeAnalyzer.Domain.Distribution;
using GenomeAnalyzer.Domain.DTO;
using GenomeAnalyzer.Domain.Response;

namespace GenomeAnalyzer.Services.Interfaces;

public interface IDistributionService
{
    Task<IBaseResponse<DistributionData>> Distribute(DistributionParamsDTO dto);
}