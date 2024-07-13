using System.Collections.Generic;

namespace SK_PG_WebApp.Models.DynamicModels
{
    public class AddUpdatePropertyDC
    {
        public string propertyName { get; set; }
        public string propertyDescription { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string location { get; set; }
        public AddUpdateProperty_POD_DC ownerDetails { get; set; } = new AddUpdateProperty_POD_DC();
        public List<AddPropertyRooms> roomDetails { get; set; } = new List<AddPropertyRooms>();
    }

    public class AddUpdateProperty_POD_DC
    {
        public string fullName { get; set; }
        public string contactNumber { get; set; }
        public string address { get; set; }
    }
    public class AddPropertyRooms
    {
        public string roomNo { get; set; } = string.Empty;
        public string pgCapacity { get; set; } = string.Empty;
        public string pgCapacityBoys { get; set; } = string.Empty;
        public string pgCapacityGirls { get; set; } = string.Empty;
        public string activePGForBoys { get; set; } = string.Empty;
        public string partiallyActivePGForBoys { get; set; } = string.Empty;
        public string pendingPGForBoys { get; set; } = string.Empty;
        public string activePGForGirls { get; set; } = string.Empty;
        public string partiallyActivePGForGirls { get; set; } = string.Empty;
        public string pendingPGForGirls { get; set; } = string.Empty;
        public string Deposit { get; set; } = string.Empty;
        public string Rent { get; set; } = string.Empty;
    }
}
