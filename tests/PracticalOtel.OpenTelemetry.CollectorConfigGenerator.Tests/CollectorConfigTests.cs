using PracticalOtel.OpenTelemetry.CollectorConfigGenerator;
using PracticalOtel.OpenTelemetry.CollectorConfigGenerator.Components;

namespace MartinDotNet.OpenTelemetry.CollectorConfigGenerator.Tests;

public class CollectorConfigTests
{
    [Fact]
    public void WithNoPipelines_EmptyConfigIsGenerated()
    {
        var config = new CollectorConfig();

        var configOutput = config.ToString();

        var expectedConfig = """
service: {}

""".ReplaceLineEndings();


        Assert.Equal(expectedConfig, configOutput);
    }

    [Fact]
    public void WithATracesPipelineWithoutComponents_ReturnsConfigWithTracesPipeline()
    {
        var config = new CollectorConfig()
            .WithPipeline(PipelineType.Traces, "");

        var configOutput = config.ToString();

        var expectedConfig = """
service:
  pipelines:
    traces: {}

""".ReplaceLineEndings();

        Assert.Equal(expectedConfig, configOutput);
    }

    [Fact]
    public void WithOLTPReceiverAndHTTPEnabled_HTTPProtocolIsLowerCase()
    {
        var otlpReceiver = new OTLPReceiver() {
            Protocols = { new HttpProtocol() }
        };

        var config = new CollectorConfig()
            .WithReceiver(otlpReceiver);

        var configOutput = config.ToString();

        var expectedConfig = """
service: {}
receivers:
  otlp:
    protocols:
      http: {}

""".ReplaceLineEndings();
            
            Assert.Equal(expectedConfig, configOutput);
        }
    
        [Fact]
        public void WithOLTPReceiverAndGRPCEnabled_GRPCProtocolIsLowerCase()
        {
            var otlpReceiver = new OTLPReceiver() {
                Protocols = { new GrpcProtocol() }
            };
    
            var config = new CollectorConfig()
                .WithReceiver(otlpReceiver);
    
            var configOutput = config.ToString();
    
            var expectedConfig = """
service: {}
receivers:
  otlp:
    protocols:
      grpc: {}

""".ReplaceLineEndings();
            Assert.Equal(expectedConfig, configOutput);
        }
        
        [Fact]
        public void WithOTLPREceiverAndBothPRotocoles_HTTPAndGRPCAreLowerCase()
        {
            var otlpReceiver = new OTLPReceiver() {
                Protocols = { new HttpProtocol(), new GrpcProtocol() }
            };
    
            var config = new CollectorConfig()
                .WithReceiver(otlpReceiver);
    
            var configOutput = config.ToString();
    
            var expectedConfig = """
service: {}
receivers:
  otlp:
    protocols:
      http: {}
      grpc: {}

""".ReplaceLineEndings();
            Assert.Equal(expectedConfig, configOutput);
        }
        
        [Fact]
        public void WithOTLPReceiverAndNoProtocols_ReturnsEmptyProtocols()
        {
            var otlpReceiver = new OTLPReceiver();
    
            var config = new CollectorConfig()
                .WithReceiver(otlpReceiver);
    
            var configOutput = config.ToString();
    
            var expectedConfig = """
service: {}
receivers:
  otlp: {}

""".ReplaceLineEndings();
            Assert.Equal(expectedConfig, configOutput);
        }
        
}