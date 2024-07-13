using System.Collections.Generic;

namespace SK_PG_WebApp.Models.DynamicModels
{
    public class PrintInvoiceDC
    {
        public string fromUser { get; set; }
        public string fromAddress { get; set; }
        public string fromEmail { get; set; }
        public string fromPhone { get; set; }

        public string toUser { get; set; }
        public string toAddress { get; set; }
        public string toEmail { get; set; }
        public string toPhone { get; set; }

        public string invoiceNumber { get; set; }

        public string totalAmount { get; set; }
        public string totalDiscount { get; set; }
        public string tax { get; set; }
        public string totalPayable { get; set; }

        public List<PrintInvoiceDataDC> data { get; set; } = new List<PrintInvoiceDataDC>();

    }


    public class PrintInvoiceDataDC
    {
        public string no { get; set; }
        public string paymentType { get; set; }
        public string dateRange { get; set; }
        public string paymentGateway { get; set; }
        public string amount { get; set; }
        public string discount { get; set; }
        public string transactionId { get; set; }
    }
}
