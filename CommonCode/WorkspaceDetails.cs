using System.Text.Json.Serialization;

namespace DockerDemo.Common.Code;

public class WorkspaceDetails
{
    [JsonPropertyName("WorkspaceId")]
    public Guid WorkspaceId { get; set; }

    [JsonPropertyName("WorkspaceName")]
    public string? WorkspaceName { get; set; }

    [JsonPropertyName("CustomerName")]
    public string? CustomerName { get; set; }
}
