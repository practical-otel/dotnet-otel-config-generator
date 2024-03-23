using System.Net.Security;
using YamlDotNet.Core.Tokens;
using YamlDotNet.Serialization;

namespace PracticalOtel.OpenTelemetry.CollectorConfigGenerator.Components;

/// <summary>
/// An OTLP Exporter that uses gRPC to send data
/// </summary>
public class OTLPExporter : BaseOTLPExporter
{
    public override string Type { get; init; } = "otlp";
    public OTLPExporter(string? name = null) : base(name) { }
}

public class TLSSettings
{
    public bool Insecure { get; internal set; } = false;
}

public abstract class BaseOTLPExporter(string? name) : Exporter(name)
{
    public string Endpoint { get; init; } = string.Empty;
    public Dictionary<string, string> Headers { get; } = [];

    [YamlIgnore]
    public bool Insecure
    {
        get
        {
            return TLS?.Insecure ?? false;
        }
        set
        {
            TLS ??= new TLSSettings();
            TLS.Insecure = value;
        }
    }

    [YamlMember(Alias = "tls")]
    public TLSSettings? TLS { get; internal set; }
}