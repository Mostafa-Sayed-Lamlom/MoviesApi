namespace MoviesApi.Dtos
{
    public class RegisterDto
    {
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [StringLength(100)]
        public string Username { get; set; } = string.Empty;

        [StringLength(128)]
        public string Email { get; set; } = string.Empty;

        [StringLength(256)]
        public string Password { get; set; } = string.Empty;
    }
}
