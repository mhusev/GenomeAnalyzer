namespace GenomeAnalyzer.Domain.Entities;

public class GenomeEntity
{
    public string Name { get; set; }
    
    public string RawGenome { get; set; }

    public GenomeEntity(string name, string rawGenome)
    {
        Name = name;
        RawGenome = rawGenome;
    }
}