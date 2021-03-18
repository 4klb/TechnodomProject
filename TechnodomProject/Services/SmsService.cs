using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;


namespace TechnodomProject.Services
{
    public class SmsService
    {
        private string accountSid = "*********";
        private string authToken = "***************";

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
