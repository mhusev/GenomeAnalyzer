using GenomeAnalyzer.Domain.Distribution;
using GenomeAnalyzer.Domain.Entities;
using GenomeAnalyzer.Domain.Response;

namespace GenomeAnalyzer.Services.Interfaces;

public interface IHomeService
{
    IQueryable<GenomeEntity> GetAll();
    Task<IBaseResponse<GenomeEntity>> Distribute(long id);
    Task<IBaseResponse<DistributionData>> GetDistributionData(DistributionParams distributionParams);
}