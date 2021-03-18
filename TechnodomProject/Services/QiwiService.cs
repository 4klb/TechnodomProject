using Qiwi.BillPayments.Model.In;
using Qiwi.BillPayments.Model;
using Qiwi.BillPayments.Client;
using Qiwi.BillPayments.Model.Out;
using System;
using TechnodomProject.Models;
using System.Diagnostics;
using System.Threading;
using TechnodomProject.Enum;

namespace TechnodomProject.Services
{
    public class QiwiService
    {

        public string Pay(User user, Purchase purchase)
        {
            BillPaymentsClient client = BillPaymentsClientFactory.Create(secretKey: "eyJ2ZXJzaW9uIjoiUDJQIiwiZGF0YSI6eyJwYXlpbl9tZXJjaGFudF9zaXRlX3VpZCI6InNkbmNjaC0wMCIsInVzZXJfaWQiOiI3NzAxMzk4OTM5NiIsInNlY3JldCI6ImVjMDc5Y2RlNmYwNTMzZTMyNDM1MzFkMDQ0ZmJhNzM2NTc0ZWI2NWZkNDUzZmUzYWViNTFmMzUzNzI1MjNhNDQifX0=");


            BillResponse form = client.CreateBill(
                info: new CreateBillInfo
                {
                    BillId = Guid.NewGuid().ToString(),
                    Amount = new MoneyAmount
                    {
                        ValueDecimal = 1.0m,
                        CurrencyEnum = CurrencyEnum.Kzt
                    },
                    Comment = "Сумма счета",
                    ExpirationDateTime = DateTime.Now.AddDays(2),
                    Customer = new Customer
                    {
                        Email = user.Email,
                        Account = Guid.NewGuid().ToString(),
                        Phone = user.Phone
                    },
                }
            );

            BillResponse responseStatus = client.GetBillInfo(billId: form.BillId);
            string status = responseStatus.Status.ValueString;

            Process myProcess = new Process();
            myProcess.StartInfo.UseShellExecute = true;
            myProcess.StartInfo.FileName = responseStatus.PayUrl.AbsoluteUri;
            myProcess.Start();


            int timer = 0;
            int timeForPay = 100000;
            int oneSecond = 1000;
            while (true)
            {
                try
                {
                    responseStatus = client.GetBillInfo(billId: form.BillId);
                    status = responseStatus.Status.ValueString;
                }
                catch
                {
                    status = Status.WAITING.ToString();
                }
                if (timer == timeForPay)
                {
                    status = Status.REJECTED.ToString();
                }
                Console.WriteLine(timer);
                Thread.Sleep(oneSecond * 5);
                timer += oneSecond * 5;
                if (status == Status.PAID.ToString())
                {
                    return status;
                }
                else if (status == Status.REJECTED.ToString())
                {
                    client.CancelBill(billId: form.BillId);
                    return status;
                }
            }
        }
    }
}
