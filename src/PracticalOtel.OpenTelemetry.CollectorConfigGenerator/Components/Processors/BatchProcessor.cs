namespace PracticalOtel.OpenTelemetry.CollectorConfigGenerator.Components;

/// <summary>
/// A processor that batches up requests beyond the ingest batches
/// </summary>
/// <param name="name"></param>
public class BatchProcessor(string? name = null) : Processor(name)
{
    public override string Type { get; init; } = "batch";
}
