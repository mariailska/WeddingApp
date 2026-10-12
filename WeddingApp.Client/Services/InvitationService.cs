using System.Net.Http.Json;
using WeddingApp.Shared.DTOs;

namespace WeddingApp.Client.Services;

public class InvitationService : IInvitationService
{
    private readonly HttpClient _httpClient;

    public InvitationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<InvitationDto?> GetInvitationAsync(string token)
    {
        var path = $"api/z/{token}";

        try
        {
            return await _httpClient.GetFromJsonAsync<InvitationDto>(path);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<bool> UpdateInvitationAsync(string token, UpdateInvitationDto updateInvitationDto)
    {
        var path = $"api/admin/z/{token}";
        HttpResponseMessage response = await _httpClient.PutAsJsonAsync(path, updateInvitationDto);
        response.EnsureSuccessStatusCode();
        return response.IsSuccessStatusCode;
    }
}