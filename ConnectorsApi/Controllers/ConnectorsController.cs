using System.Text.Json;
using DockerDemo.Common.Code;
using Microsoft.AspNetCore.Mvc;

namespace ConnectorsApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ConnectorsController : ControllerBase
{
    private static readonly Dictionary<Guid, Connector[]> ConnectorsPerWsId = new Dictionary<Guid, Connector[]>()
    {
        {
            Guid.Parse("695e635b-da8c-4f31-a10e-cbe8492dd141"),
            new Connector[] { new Connector() { ConnectorId = 1, ConnectorName = "Connector1"   } }
        },
        {
            Guid.Parse("7685007a-9ca9-4056-a123-7d7ba88b522a"),
            new Connector[] {
                new Connector() { ConnectorId = 1, ConnectorName = "Connector1"   } ,
                new Connector() { ConnectorId = 2, ConnectorName = "Connector2"   }
             }
        }
    };

    private readonly ILogger<ConnectorsController> _logger;


    public ConnectorsController(ILogger<ConnectorsController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetConnectorsByWorkspaceName/{WorkspaceName}")]
    public async Task<IEnumerable<Connector>> Get(string workspaceName)
    {
        var wsId = await GetWorkspaceIdByNameAsync(workspaceName);

        ConnectorsPerWsId.TryGetValue(wsId, out var workspaceConnectors);
        if (workspaceConnectors == null)
        {
            throw new HttpRequestException();
        }

        return workspaceConnectors;
    }

    private async Task<Guid> GetWorkspaceIdByNameAsync(string workspaceName)
    {
        Console.WriteLine($"Workspace Name: {workspaceName}");
        string url = $"http://{GetWorkspaceMetadataServiceDomain()}:{GetWorkspaceMetadataServicePort()}/WorkspaceMetadata?Name={workspaceName}";
        Console.WriteLine($"URL: {url}");

        using var client = new HttpClient();

        var response = await client.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Error while making request");
        }
        var responseContent = await response.Content.ReadAsStringAsync();

        Console.WriteLine($"Response content: {responseContent}");
        WorkspaceDetails ws = JsonSerializer.Deserialize<WorkspaceDetails>(responseContent);
        return ws.WorkspaceId;
    }

    private string GetWorkspaceMetadataServiceDomain()
    {
        return Environment.GetEnvironmentVariable("WORKSPACE_METADATA_SERVICE_DOMAIN") ?? "localhost";
    }

    private string GetWorkspaceMetadataServicePort()
    {
        return Environment.GetEnvironmentVariable("WORKSPACE_METADATA_SERVICE_PORT") ?? "5000";
    }
}
