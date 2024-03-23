using YamlDotNet.Serialization;

namespace PracticalOtel.OpenTelemetry.CollectorConfigGenerator.Components;

/// <summary>
/// Receiver for OTLP data that supports HTTP and gRPC.
/// </summary>
public class OTLPReceiver : Receiver
{
    public override string Type { get; init; } = "otlp";
    public OTLPReceiver(string? name = null) : base(name) { }

    /// <summary>
    /// The supported Protocols
    /// </summary>
    public Dictionary<string, Protocol> Protocols { get; } = new();
}

public static class OTLPReceiverDictionaryExtensions
{
    public static void Add(this Dictionary<string, Protocol> dictionary, Protocol protocol)
    {
        if (dictionary.ContainsKey(protocol.Type))
            throw new InvalidOperationException($"Protocol with type {protocol.Type} already exists in the dictionary.");

        dictionary.Add(protocol.Type, protocol);
    }
}

public abstract class Protocol
{
    public static Protocol Http => new HttpProtocol();
    public static Protocol Grpc => new GrpcProtocol();

    [YamlIgnore]
    public abstract string Type { get; init; }
}


public class HttpProtocol : Protocol
{
    public override string Type { get; init; } = "http";
}

public class GrpcProtocol : Protocol
{
    public override string Type { get; init; } = "grpc";
}
