namespace GenomeAnalyzer.Domain.DTO;

public class DistributionParamsDTO
{
    public long Id { get; set; }
    public char? Nucleotide { get; set; }
    public int? SequenceLength { get; set; }
    public int? StartPosition { get; set; }
}