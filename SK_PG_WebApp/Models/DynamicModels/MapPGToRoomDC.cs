using System.Collections.Generic;

namespace SK_PG_WebApp.Models.DynamicModels
{
    public class MapPGToRoomDC
    {
        public string name { get; set; }
        public string userId { get; set; }
        public string cityId { get; set; }
        public string city { get; set; }
        public string location { get; set; }
        public string locationId { get; set; }
        public string property { get; set; }
        public string propertyId { get; set; }
        public string boysAvailable { get; set; }
        public string girlsAvailable { get; set; }
        public List<MapPGToRoomDCDetails> pgRoomDetails { get; set; } = new List<MapPGToRoomDCDetails>();
        
    }

    public class MapPGToRoomDCDetails
    {
        public string roomId { get; set; }
        public string roomName { get; set; }
        public string capacity { get; set; }
        public string rent { get; set; }
        public string deposit { get; set; }
        public string allocatedTo { get; set; }
        public string backgroundColour { get; set; }
        public string isAvailable { get; set; }

        public string allocation { get; set; }
        public decimal availability { get; set; }
    }
}
