namespace GenomeAnalyzer.Domain.Distribution;

public static class DistributionHelper
{
    public static DistributionData DistributeGenomeByAdenine(string genome)
    {
        return DistributeGenomeByNucleotide(genome, 'a');
    }
    
    public static DistributionData DistributeGenomeByCytosine(string genome)
    {
        return DistributeGenomeByNucleotide(genome, 'c');
    }
    
    public static DistributionData DistributeGenomeByGuanine(string genome)
    {
        return DistributeGenomeByNucleotide(genome, 'g');
    }
    
    public static DistributionData DistributeGenomeByThymine(string genome)
    {
        return DistributeGenomeByNucleotide(genome, 't');
    }
    
    public static DistributionData DistributeGenomeByConstantLength(string genome, int length, int position)
    {
        string normalizedGenome = NormalizeGenomeByLength(genome, length);

        position--;
        
        genome = genome
            .Substring(position, genome.Length - position) 
                 + genome.Substring(0, position);

        string[] sequences = SplitInParts(genome, length).ToArray();

        return DoStatisticalCalculations(sequences, genome);
    }
    
    public static DistributionData DistributeGenomeByNgram(string genome, int size)
    {
        return DoStatisticalCalculations(ObtainNgram(genome, size).ToArray(), genome);
    }

    private static DistributionData DistributeGenomeByNucleotide(string genome, char n)
    {
        string rawGenome = genome;
        genome = genome.Replace(n, ' ');

        for (int i = 0; i < genome.Length - 1; i++)
        {
            if (genome[i] == ' ' && genome[i + 1] == ' ')
            {
                genome = genome.Insert(i + 1, "x");
            }
        }

        if (genome.Last() == ' ')
        {
            genome = genome.Remove(genome.Length - 1, 1);
        }

        if (genome[0] == ' ')
        {
            genome = genome.Remove(0, 1);
        }

        return DoStatisticalCalculations(genome.Split(' '), rawGenome);
        
        /*string[] sequences = genome
            .Replace(n, ' ')
            .Replace("  ", " x ")
            .Split(' ')
            .Where(seq => seq.Length != 0 && seq != " ")
            .ToArray();

        return DoStatisticalCalculations(sequences, genome);*/
    }

    private static DistributionData DoStatisticalCalculations(string[] sequences, string genome)
    {
        int adenineAmount = genome.Count(c => c == 'a');
        int cytosineAmount = genome.Count(c => c == 'c');
        int guanineAmount = genome.Count(c => c == 'g');
        int thymineAmount = genome.Count(c => c == 't');
        int nucleotidesAmount = genome.Length;
        
        int sequencesAmount = sequences.Length;

        Dictionary<string, int> sequencesFrequency = CalculateNucleotideSequencesFrequency(sequences);
        Dictionary<int, int> rankFrequency = CalculateRankFrequency(sequencesFrequency);
        Dictionary<int, int> statisticalSpectrum = CalculateStatisticalSpectrum(sequencesFrequency);

        double entropy = CalculateEntropy(rankFrequency);
        double firstCentralMoment = CalculateFirstCentralMoment(sequences, sequencesAmount);
        double secondCentralMoment = CalculateSecondCentralMoment(sequences, sequencesAmount, firstCentralMoment);
        double dispersionCoefficient = CalculateDispersionCoefficient(firstCentralMoment, secondCentralMoment);

        return new DistributionData()
        {
            AdenineAmount = adenineAmount,
            CytosineAmount = cytosineAmount,
            GuanineAmount = guanineAmount,
            ThymineAmount = thymineAmount,
            NucleotidesAmount = nucleotidesAmount,
            SequencesAmount = sequencesAmount,
            SequencesFrequency = sequencesFrequency,
            RankFrequency = rankFrequency,
            StatisticalSpectrum = statisticalSpectrum,
            Entropy = entropy,
            FirstCentralMoment = firstCentralMoment,
            SecondCentralMoment = secondCentralMoment,
            DispersionCoefficient = dispersionCoefficient
        };
    }
    
    private static IEnumerable<string> SplitInParts(string s, int length)
    {
        for (var i = 0; i < s.Length; i += length)
            yield return s.Substring(i, Math.Min(length, s.Length - i));
    }
    
    private static IEnumerable<string> ObtainNgram(string genome, int size)
    {
        genome = NormalizeGenomeByLength(genome, size);
        
        for (int i = 0; i < genome.Length - size + 1; i+= size)
        {
            yield return genome.Substring(i, size);
        }
    }
    
    private static string NormalizeGenomeByLength(string genome, int length)
    {
        if (genome.Length % length != 0)
        {
            for (int i = 0; genome.Length % length != 0; i++)
            {
                genome += genome[i];
            }
        }

        return genome;
    }
    
    private static double CalculateEntropy(IDictionary<int, int> rankFrequency)
    {
        double entropy = 0, sumOfFrequencies = 0;

        foreach (int frequency in rankFrequency.Values)
        {
            sumOfFrequencies += frequency;
        }

        foreach (int frequency in rankFrequency.Values)
        {
            entropy += frequency / sumOfFrequencies * Math.Log(frequency / sumOfFrequencies);
        }
        
        return -entropy;
    }
    
    private static double CalculateFirstCentralMoment(string[] sequences, int sequencesAmount)
    {
        double firstCentralMoment = 0;

        foreach (var seq in sequences)
        {
            if (seq != "x")
            {
                firstCentralMoment += seq.Length;
            }
        }

        return firstCentralMoment / sequencesAmount;
    }
    
    private static double CalculateSecondCentralMoment(string[] sequences, int sequencesAmount, double firstCentralMoment)
    {
        double secondCentralMoment = 0;

        foreach (var seq in sequences)
        {
            secondCentralMoment += Math.Pow((seq != "x" ? 0 : seq.Length) - firstCentralMoment, 2);
        }
        
        return secondCentralMoment / sequencesAmount;
    }
    
    private static double CalculateDispersionCoefficient(double firstCentralMoment, double secondCentralMoment)
    {
        return secondCentralMoment / (firstCentralMoment - 1);
    }
    
    private static Dictionary<string, int> CalculateNucleotideSequencesFrequency(string[] sequences)
    {
        Dictionary<string, int> sequencesFrequency = new Dictionary<string, int>();

        foreach (var seq in sequences)
        {
            if (sequencesFrequency.ContainsKey(seq))
            {
                sequencesFrequency[seq]++;
            }
            else
            {
                sequencesFrequency[seq] = 1;
            }
        }

        return sequencesFrequency
            .OrderByDescending(pair => pair.Value)
            .ToDictionary(pair => pair.Key, pair => pair.Value);
    }
    
    private static Dictionary<int, int> CalculateRankFrequency(IDictionary<string, int> sequencesFrequency)
    {
        Dictionary<int, int> rankFrequency = new Dictionary<int, int>();

        int rank = 1;
        foreach (var key in sequencesFrequency.Keys)
        {
            rankFrequency[rank] = sequencesFrequency[key];
            rank++;
        }

        return rankFrequency
            .OrderByDescending(pair => pair.Value)
            .ToDictionary(pair => pair.Key, pair => pair.Value);;
    }
    
    private static Dictionary<int, int> CalculateStatisticalSpectrum(IDictionary<string, int> sequencesFrequency)
    {
        Dictionary<int, int> statisticalSpectrum = new Dictionary<int, int>();

        foreach (var freq in sequencesFrequency.Values)
        {
            if (statisticalSpectrum.ContainsKey(freq))
            {
                statisticalSpectrum[freq]++;
            }
            else
            {
                statisticalSpectrum[freq] = 1;
            }
        }

        return statisticalSpectrum
            .OrderByDescending(pair => pair.Value)
            .ToDictionary(pair => pair.Key, pair => pair.Value);;
    }
}