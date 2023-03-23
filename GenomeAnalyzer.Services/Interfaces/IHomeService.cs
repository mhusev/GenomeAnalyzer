using GenomeAnalyzer.Domain.Entities;
using GenomeAnalyzer.Domain.Response;
using GenomeAnalyzer.Domain.ViewModels;

namespace GenomeAnalyzer.Services.Interfaces;

public interface IHomeService
{
    IQueryable<GenomeEntity> GetAll();
    Task<IBaseResponse<GenomeEntity>> Create(CreateGenomeViewModel model);
}