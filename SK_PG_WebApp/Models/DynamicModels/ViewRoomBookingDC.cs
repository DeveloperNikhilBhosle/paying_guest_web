using System.Collections.Generic;

namespace SK_PG_WebApp.Models.DynamicModels
{
    public class ViewRoomBookingDC
    {
        public string roomId { get; set; }
        public string name { get; set; }
        public string allocatedTo { get; set; }
        public string backgroundColour { get; set; }
        public string rentAmount { get; set; }
        public string depositAmount { get; set; }
        public string pgCapacity { get; set; }
        public List<ViewRoomBookingPGDC> pgDetails { get; set; } = new List<ViewRoomBookingPGDC>();

    }

    public class ViewRoomBookingPGDC
    {
        public string name { get; set; }
        public string whatsAppNumber { get; set; }
        public string nextRenewDate { get; set; }
        public string backgroundColour { get; set; }

    }
}
