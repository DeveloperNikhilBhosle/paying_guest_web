namespace SK_PG_WebApp.Models.DynamicModels
{
    public class PropertyDetailsDashboardDC
    {
        public string propertyId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string owner { get; set; }
        public string ownerNumber { get; set; }
        public string totalTenant { get; set; }
        public string totalNP { get; set; }
        public string totalPaymentCollection { get; set; }

    }

    public class PropertyDetailsDashboard_PGFloorDC
    {
        public string name { get; set; }
        public string totalPG { get; set; }
        public string totalRooms { get; set; }
        public string background { get; set; }
        public string id { get; set; }
        public string visibilityRemoveButton { get; set; }
    }
}
