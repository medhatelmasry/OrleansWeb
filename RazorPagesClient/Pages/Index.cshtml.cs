using GrainInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Orleans;

namespace RazorClient.Pages;

public class IndexModel : PageModel {
    private readonly ILogger<IndexModel> _logger;
    private readonly IClusterClient _clusterClient;
    public string Message { get; set; } = "";

    [BindProperty]
    public string? Key { get; set; }

    public IndexModel(ILogger<IndexModel> logger, IClusterClient clusterClient) {
        _logger = logger;
        _clusterClient = clusterClient;
    }

    public void OnGet() { }

    public async Task OnPostDecrement(int count)
    {
        var counterGrain = _clusterClient.GetGrain<ICounterGrain>(Key);
        Message = await counterGrain.Decrement(count);
    }

    public async Task OnPostIncrement(int count) {
        var counterGrain = _clusterClient.GetGrain<ICounterGrain>(Key);
        Message = await counterGrain.Increment(count);
    }
    public async Task OnPostCount() {
        var counterGrain = _clusterClient.GetGrain<ICounterGrain>(Key);
        var currentCount = await counterGrain.GetCount();

        Message = $"Current value of count for '{Key}' is {currentCount}";
    }

    public void OnPostKey(string key) {
        Key = key;
        Message = $"Current value of key is '{Key}'";
    }
}
