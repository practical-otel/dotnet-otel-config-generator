namespace PracticalOtel.OpenTelemetry.CollectorConfigGenerator;

public static class CollectorConfigExtensions
{
    /// <summary>
    /// Adds a new Processor to the config
    /// </summary>
    /// <param name="config"></param>
    /// <param name="component"></param>
    /// <returns></returns>
    public static CollectorConfig WithProcessor(this CollectorConfig config, Processor component)
    {
        config.Processors.Add(component.GetName(), component);
        return config;
    }

    /// <summary>
    /// Adds a new Exporter to the config
    /// </summary>
    /// <param name="config"></param>
    /// <param name="component"></param>
    /// <returns></returns>
    public static CollectorConfig WithExporter(this CollectorConfig config, Exporter component)
    {
        config.Exporters.Add(component.GetName(), component);
        return config;
    }

    /// <summary>
    /// Adds a new Receiver to the config
    /// </summary>
    /// <param name="config"></param>
    /// <param name="component"></param>
    /// <returns></returns>
    public static CollectorConfig WithReceiver(this CollectorConfig config, Receiver component)
    {
        config.Receivers.Add(component.GetName(), component);
        return config;
    }

    /// <summary>
    /// Adds a new Pipeline to the config
    /// 
    /// This will add exporters and processors to the config if they are not already present
    /// </summary>
    /// <param name="config"></param>
    /// <param name="type"></param>
    /// <param name="name"></param>
    /// <param name="exporters"></param>
    /// <param name="processors"></param>
    /// <param name="receivers"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">Throws an exception if there is a pipeline with this name and type already added.</exception>
    public static CollectorConfig WithPipeline(this CollectorConfig config, PipelineType type, string name,
        List<Exporter>? exporters = null, List<Processor>? processors = null, List<Receiver>? receivers = null)
    {
        if (config.Service.Pipelines.ContainsKey($"{type.ToString().ToLower()}/{name}"))
            throw new ArgumentException($"Pipeline {type.ToString().ToLower()}/{name} already exists", nameof(name));

        if (exporters != null)
            foreach (var exporter in exporters)
                config.Exporters.TryAdd(exporter.GetName(), exporter);

        if (processors != null)
            foreach (var processor in processors)
                config.Processors.TryAdd(processor.GetName(), processor);

        if (receivers != null)  
            foreach (var receiver in receivers)
                config.Receivers.TryAdd(receiver.GetName(), receiver);

        var pipeline = new CollectorPipeline(type, name) { 
            Receivers = receivers?.Select(r => r.GetName()).ToList() ?? [],
            Processors = processors?.Select(p => p.GetName()).ToList() ?? [],
            Exporters = exporters?.Select(e => e.GetName()).ToList() ?? []
        };
        config.Service.Pipelines.Add(pipeline.GetName(), pipeline);
        return config;
    }
}
