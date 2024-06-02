namespace MoviesApi.Dtos
{
    public class SendSmsDto
    {
        [RegularExpression(@"^\+201[0125][0-9]{8}$", ErrorMessage = "The entered number is not a valid Egyptian phone number.")]
        public string MobileNumber { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}
