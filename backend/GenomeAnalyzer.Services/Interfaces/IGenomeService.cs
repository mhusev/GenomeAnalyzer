using GenomeAnalyzer.Domain.DTO;
using GenomeAnalyzer.Domain.Entities;
using GenomeAnalyzer.Domain.Response;

namespace GenomeAnalyzer.Services.Interfaces;

public interface IGenomeService
{
    Task<IBaseResponse<GenomeEntity>> Get(long id);
    Task<IBaseResponse<GenomeEntity>> Edit(GenomeEntity entity);
    Task<IBaseResponse<GenomeEntity>> Create(CreateGenomeDTO model);
    Task<IBaseResponse<object>> Delete(long id);
}