using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace PracticalOtel.OpenTelemetry.CollectorConfigGenerator;

/// <summary>
/// OpenTelemetry Collector Config
/// </summary>
public class CollectorConfig
{
    /// <summary>
    /// Services Section of the Config
    /// </summary>
    public CollectorService Service { get; } = new CollectorService();
    public Dictionary<string, Receiver> Receivers { get; } = [];
    public Dictionary<string, Processor> Processors { get; } = [];
    public Dictionary<string, Exporter> Exporters { get; } = [];

    public override string ToString()
    {
        var serializer = new SerializerBuilder()
            .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitEmptyCollections)
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
        return serializer.Serialize(this);
    }
}

public class CollectorService
{
    public Dictionary<string, CollectorPipeline> Pipelines { get; } = [];
}

/// <summary>
/// Pipelines Configuration, this is what will config different components to work together.
/// </summary>
/// <param name="type">The Type of the pipeline, e.g. Traces, Logs or Metrics</param>
/// <param name="name"></param>
public class CollectorPipeline(PipelineType type, string name = "")
{
    [YamlIgnore]
    public string Name { get; init; } = name;
    [YamlIgnore]
    public PipelineType Type { get; init; } = type;

    /// <summary>
    /// Receivers to use in the pipeline
    /// </summary>
    public List<string> Receivers { get; init; } = [];
    
    /// <summary>
    /// Processors to use in the pipeline
    /// </summary>
    public List<string> Processors { get; init; } = [];

    /// <summary>
    /// Exporters to use in the pipeline
    /// </summary>
    public List<string> Exporters { get; init; } = [];

    /// <summary>
    /// Get the name of the pipeline for use in the YAML (concatenated type and name)
    /// </summary>
    /// <returns>The pipeline name</returns>
    public string GetName()
    {
        return string.IsNullOrEmpty(Name) ? Type.ToString().ToLower() : $"{Type.ToString().ToLower()}/{Name}";
    }
}


/// <summary>
/// Base class for all components
/// </summary>
/// <param name="name">The unique name for this component</param>
public abstract class Component(string? name)
{
    [YamlIgnore]
    public virtual string Type { get; init; } = string.Empty;
    [YamlIgnore]
    public virtual string Name { get; init; } = name ?? string.Empty;

    public string GetName()
    {
        return string.IsNullOrEmpty(Name) ? Type.ToLower() : $"{Type.ToLower()}/{Name}";
    }
}


/// <summary>
/// Base class for all Exporters
/// </summary>
/// <param name="name">The unique name for this exporter</param>
public abstract class Exporter(string? name) : Component(name)
{
}

/// <summary>
/// Base class for all Processors
/// </summary>
/// <param name="name">The unique name for this processor</param>
public abstract class Processor(string? name) : Component(name)
{
}

/// <summary>
/// Base class for all Receivers
/// </summary>
/// <param name="name">The unique name for this receiver</param>
public abstract class Receiver(string? name) : Component(name)
{
}
