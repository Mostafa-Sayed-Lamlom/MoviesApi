using Microsoft.Extensions.Options;
using MoviesApi.Helper;
using MoviesApi.Services.Interfaces;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace MoviesApi.Services.Implementations
{
    public class SmsService : ISmsService
    {
        private readonly TwilioSettings _twilio;
        public SmsService(IOptions<TwilioSettings> twilio)
        {
            _twilio = twilio.Value;
        }
        public MessageResource Send(string mobileNumber, string body)
        {
            TwilioClient.Init(_twilio.AccountSID, _twilio.AuthToken);

            var result = MessageResource.Create(
                    body: body,
                    from: new Twilio.Types.PhoneNumber(_twilio.TwilioPhoneNumber),
                    to: mobileNumber
                );

            return result;
        }
    }
}
