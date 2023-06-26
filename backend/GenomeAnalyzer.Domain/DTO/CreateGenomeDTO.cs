using GenomeAnalyzer.Domain.Enum;

namespace GenomeAnalyzer.Domain.DTO;

public class CreateGenomeDTO
{
    public string Name { get; set; }
    public SpeciesType Type { get; set; }
    public string RawGenome { get; set; }
}