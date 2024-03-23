// See https://aka.ms/new-console-template for more information

using PracticalOtel.OpenTelemetry.CollectorConfigGenerator;
using PracticalOtel.OpenTelemetry.CollectorConfigGenerator.Components;

var otlpReceiver = new OTLPReceiver() {
    Protocols = { Protocol.Http, Protocol.Grpc },
};

var otlpExporter = new OTLPExporter("aspire") {
    Endpoint = "${ASPIRE_ENDPOINT}",
    Insecure = true,
};

var batchProcessor = new BatchProcessor();

var config = new CollectorConfig()
    .WithPipeline(PipelineType.Traces, "default", [ otlpExporter ], [batchProcessor], [ otlpReceiver ])
    .WithPipeline(PipelineType.Metrics, "default", [ otlpExporter ], [batchProcessor], [ otlpReceiver ])
    .WithPipeline(PipelineType.Logs, "default", [ otlpExporter ], [batchProcessor], [ otlpReceiver ]);

Console.WriteLine(config.ToString());

