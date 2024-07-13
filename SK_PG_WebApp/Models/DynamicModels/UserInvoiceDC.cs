namespace SK_PG_WebApp.Models.DynamicModels
{
    public class UserInvoiceDC
    {
        public string paymentId { get; set; }
        public string amount { get; set; }
        public string discount { get; set; }
        public string paymentDescription { get; set; }
        public string paymentDate { get; set; }
        public string paymentDateRange { get; set; }
    }
}
