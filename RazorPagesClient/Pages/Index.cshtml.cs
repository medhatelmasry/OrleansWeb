using GrainInterfaces;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesClient.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IClusterClient _clusterClient;

    public IndexModel(ILogger<IndexModel> logger,
        IClusterClient clusterClient)
    {
        _logger = logger;
        _clusterClient = clusterClient;
    }

    public async Task OnGet()
    {
        var result = await _clusterClient.GetGrain<IPersonGrain>("Fred").SayHelloAsync();
        ViewData["response"] = result;
    }
}
