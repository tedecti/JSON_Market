using JSON_Market.Models.User;

namespace JSON_Market.Services.Interfaces;

public interface IAuthService
{
    Task<string?> RegisterFromMicroservice(UserDto userDto);
}