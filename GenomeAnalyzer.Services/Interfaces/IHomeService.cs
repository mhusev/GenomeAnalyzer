using GenomeAnalyzer.Domain.Entities;
using GenomeAnalyzer.Domain.Response;

namespace GenomeAnalyzer.Services.Interfaces;

public interface IHomeService
{
    Task<IBaseResponse<GenomeEntity>> Create(GenomeEntity entity);
}