namespace MoviesApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthDto> RegisterAsync(RegisterDto registerDto);
        Task<AuthDto> LoginAsync(LoginDto loginDto);
        Task<string> AddRoleAsync(AddRoleDto addRoleDto);
        Task<AuthDto> RefreshTokenAsync(string token);
        Task<bool> RevokeTokenAsync(string token);
    }
}
