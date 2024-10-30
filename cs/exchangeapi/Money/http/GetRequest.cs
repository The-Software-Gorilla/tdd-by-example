using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace TheSoftwareGorilla.TDD.Money.Http;

public class GetRequest
{
    private readonly string _url;

    public GetRequest(string url)
    {
        _url = url;

    }

    public async Task<string> FetchResponseAsync()
    {
        try
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(_url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error fetching response: {0}", e.Message);
            return null;
        }
    }

    public string Url => _url;
}