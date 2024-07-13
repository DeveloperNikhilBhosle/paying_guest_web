namespace SK_PG_WebApp.Models.DynamicModels
{
    public class AddPaymentSuccessDC
    {
        public string fromUserName { get; set; }
        public string toUsername { get; set; }
        public string amount { get; set; }
        public string discount { get; set; }
        public string paymentGateway { get; set; }
        public string dateRange { get; set; }
        public string paymentType { get; set; }
        public string status { get; set; } = "SUCCESS";
        public string paymentDetails { get; set; }

    }
}
