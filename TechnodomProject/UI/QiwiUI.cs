using Qiwi.BillPayments.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TechnodomProject.UI
{
    public class QiwiUI
    {
        public void Pay(BillPaymentsClient client)
        {
            Process myProcess = new Process();

            string status = responseStatus.Status.ValueString;
            myProcess.StartInfo.UseShellExecute = true;
            myProcess.StartInfo.FileName = responseStatus.PayUrl.AbsoluteUri;
            myProcess.Start();
        }
    }
}
