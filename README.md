# OpenTelemetry Collector Config generator

This is a library that allows you to create OpenTelemetry Collector configs with some type safety and validity checks.

This will allow you to do code like this:

```csharp
var otlpReceiver = new OTLPReceiver() {
    Protocols = { Protocol.Http, Protocol.Grpc },
};

var otlpExporter = new OTLPExporter("aspire") {
    Endpoint = "${ASPIRE_ENDPOINT}",
    Insecure = true,
};

var batchProcessor = new BatchProcessor();

var config = new CollectorConfig()
    .WithPipeline(PipelineType.Traces, "default", [ otlpExporter ], [ batchProcessor ], [ otlpReceiver ])
    .WithPipeline(PipelineType.Metrics, "default", [ otlpExporter ], [ batchProcessor ], [ otlpReceiver ])
    .WithPipeline(PipelineType.Logs, "default", [ otlpExporter ], [ batchProcessor ], [ otlpReceiver ]);
```

As the config is rather verbose, this is useful for scenarios where you want to generate these in a pipeline.

This was originally developed as a helper for the Aspire Collector plugin.