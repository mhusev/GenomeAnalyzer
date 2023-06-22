using GenomeAnalyzer.Domain.Enum;

namespace GenomeAnalyzer.Domain.Response;

public class BaseResponse<T> : IBaseResponse<T>
{
    public string Description { get; set; }
    public StatusCode StatusCode { get; set; }
    public T Data { get; set; }
}