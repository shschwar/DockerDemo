using System.Net;
using DockerDemo.Common.Code;
using Microsoft.AspNetCore.Mvc;

namespace WorkspaceMetadataService.Controllers;

[ApiController]
[Route("[controller]")]
public class WorkspaceMetadataController : ControllerBase
{
    private static readonly WorkspaceDetails[] _workspacesList = new[]
    {
        new WorkspaceDetails() { WorkspaceId = Guid.Parse("695e635b-da8c-4f31-a10e-cbe8492dd141"), WorkspaceName = "LAW", CustomerName = "S&P 500 Company" },
        new WorkspaceDetails() { WorkspaceId = Guid.Parse("7685007a-9ca9-4056-a123-7d7ba88b522a"), WorkspaceName = "Sentinel-Wal", CustomerName = "Another S&P 500 Company" },
    };

    private readonly ILogger<WorkspaceMetadataController> _logger;

    public WorkspaceMetadataController(ILogger<WorkspaceMetadataController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWorkspaceByName/{Name}")]
    public WorkspaceDetails Get(string Name)
    {
        var workspace = _workspacesList.Where(workspace => workspace.WorkspaceName.Equals(Name)).SingleOrDefault();
        if (workspace == null)
        {
            throw new HttpRequestException(
                message: $"Workspace {Name} is not found",
                statusCode: HttpStatusCode.NotFound,
                inner: null
                );
        }

        return workspace;
    }
}
