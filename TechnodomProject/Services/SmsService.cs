using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;


namespace TechnodomProject.Services
{
    public class SmsService
    {
        private string accountSid = "AC03798a1882921c7f150d85a1f9967321";
        private string authToken = "9401c6e36f47d4613c0d4f35733de6f2";

        public SmsService()
        {
            TwilioClient.Init(accountSid, authToken);
        }

        public int SendCode(string phone)
        {
            Random random = new Random();
            int randomCode = random.Next(100000, 999999);

            var to = new PhoneNumber(phone);
            var from = new PhoneNumber("+12059473430");

            var message = MessageResource.Create(
                to: to,
                from: from,
                body: randomCode.ToString()
            );

            return randomCode;
        }
    }
}
