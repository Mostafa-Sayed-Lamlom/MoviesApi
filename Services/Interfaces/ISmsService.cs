using Twilio.Rest.Api.V2010.Account;

namespace MoviesApi.Services.Interfaces
{
    public interface ISmsService
    {
        MessageResource Send(string mobileNumber, string body);
    }
}
