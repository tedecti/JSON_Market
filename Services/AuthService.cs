using JSON_Market.Models.User;
using JSON_Market.Services.Interfaces;

namespace JSON_Market.Services;

public class AuthService : IAuthService
{
    private readonly IHttpClientFactory _client;

    public AuthService(IHttpClientFactory client)
    {
        _client = client;
    }

    public async Task<string?> RegisterFromMicroservice(UserDto userDto)
    {
        var client = _client.CreateClient("AuthMicroservice");
        var response = await client.PostAsJsonAsync("/signup", userDto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}