using GenomeAnalyzer.Domain.Enum;

namespace GenomeAnalyzer.Domain.ViewModels;

public class CreateGenomeViewModel
{
    public string Name { get; set; }
    
    public SpeciesType Type { get; set; }
    
    public string RawGenome { get; set; }
}