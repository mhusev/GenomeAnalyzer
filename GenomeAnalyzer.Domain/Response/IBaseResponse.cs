using GenomeAnalyzer.Domain.Enum;

namespace GenomeAnalyzer.Domain.Response;

public interface IBaseResponse<T>
{
    string Description { get; set; }
    
    StatusCode StatusCode { get; set; } 
    
    T Data { get; set; }
}