namespace GenomeAnalyzer.Domain.Distribution;

public class DistributionParams
{
    public long Id { get; set; }
    
    public char? Nucleotide { get; set; }
    
    public int? SequenceLength { get; set; }
    
    public int? StartPosition { get; set; }
}