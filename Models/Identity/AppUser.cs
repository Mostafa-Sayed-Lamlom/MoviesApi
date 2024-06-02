using Microsoft.AspNetCore.Identity;

namespace MoviesApi.Models.Identity
{
    public class AppUser : IdentityUser
    {
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;
        public List<RefreshToken>? RefreshTokens { get; set; }
    }
}
