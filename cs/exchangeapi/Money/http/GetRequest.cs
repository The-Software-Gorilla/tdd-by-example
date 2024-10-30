using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace TheSoftwareGorilla.TDD.Money.Http;

public class GetRequest
{
    public string Url { get; }

    public GetRequest(string url)
    {
        Url = url;

    }

    public async Task<string> FetchResponseAsync()
    {
        using HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync(Url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }


}