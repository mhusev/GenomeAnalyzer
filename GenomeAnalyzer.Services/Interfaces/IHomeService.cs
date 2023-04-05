using GenomeAnalyzer.Domain.Entities;
using GenomeAnalyzer.Domain.Response;
using GenomeAnalyzer.Domain.ViewModels;

namespace GenomeAnalyzer.Services.Interfaces;

public interface IHomeService
{
    Task<IBaseResponse<GenomeEntity>> GetGenome(long id);
    Task<IBaseResponse<GenomeEntity>> Edit(GenomeEntity entity);
    IQueryable<GenomeEntity> GetAll();
    Task<IBaseResponse<GenomeEntity>> Create(CreateGenomeViewModel model);
    Task<IBaseResponse<bool>> Delete(long id);
}