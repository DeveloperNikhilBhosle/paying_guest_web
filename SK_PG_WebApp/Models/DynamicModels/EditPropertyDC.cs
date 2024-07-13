namespace SK_PG_WebApp.Models.DynamicModels
{
    public class EditPropertyDC
    {
        public int propertyId { get; set; }
        public string propertyName { get; set; }
        public string propertyDescription { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string location { get; set; }

        // Owner Details
        public string fullName { get; set; }
        public string contactNumber { get; set; }
        public string address { get; set; }
    }
}
