namespace PracticalOtel.OpenTelemetry.CollectorConfigGenerator.Components;

/// <summary>
/// An OTLP Exporter that uses HTTP Protobuf to send data
/// </summary>
public class OTLPHTTPExporter : BaseOTLPExporter
{
    public override string Type { get; init; } = "otlphttp";
    public OTLPHTTPExporter(string? name = null) : base(name) { }
}
