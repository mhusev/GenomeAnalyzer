namespace GenomeAnalyzer.Domain.Distribution;

public class DistibutionData
{
    public string DistributedGenome { get; set; }
    
    public int AdenineAmount { get; set; }
    
    public int CytosineAmount { get; set; }
    
    public int GuanineAmount { get; set; }
    
    public int ThymineAmount { get; set; }
    
    public int NucleotidesAmount { get; set; }
    
    public int SequencesAmount { get; set; }
    
    public double Entropy { get; set; }
    
    public double FirstCentralMoment { get; set; }
    
    public double SecondCentralMoment { get; set; }
    
    public double DispersionCoefficient { get; set; }
    
    public double T { get; set; }
    
    public double Kappa { get; set; }
    
    public IDictionary<string, int> SequencesFrequency { get; set; }
    
    public IDictionary<int, int> RankFrequency { get; set; }
    
    public IDictionary<int, int> StatisticalSpectrum { get; set; }
}