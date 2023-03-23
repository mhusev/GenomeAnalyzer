using GenomeAnalyzer.Domain.Enum;

namespace GenomeAnalyzer.Domain.Entities;

public class GenomeEntity
{
    public long Id { get; set; }
    
    public string Name { get; set; }
    
    public SpeciesType Type { get; set; }
    
    public string RawGenome { get; set; }

    public GenomeEntity(string name, SpeciesType type, string rawGenome)
    {
        Name = name;
        Type = type;
        RawGenome = rawGenome;
    }
}