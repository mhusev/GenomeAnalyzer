using GenomeAnalyzer.Domain.Entities;

namespace GenomeAnalyzer.Services.Interfaces;

public interface IHomeService
{
    IQueryable<GenomeEntity> GetAll();
}