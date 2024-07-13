using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SK_PG_WebApp.Models.BusinessModels
{
    [Table("pgroomsmaster")]
    public class PGRoomMasterBO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 id { get; set; }
        public string name { get; set; }
        public Int64? pgMasterId { get; set; }
        public Int64? noOfHall { get; set; }
        public Int64? noOfBedroom { get; set; }
        public Int64? noOfKitchen { get; set; }
        public Int64? pgCapacity { get; set; }
        public Int64? activePG { get; set; }
        public Int64? availablePG { get; set; }
        public Int64? noticePeriodMonths { get; set; }
        public Int64? noticePeriodDays { get; set; }
        public decimal? depositAmount { get; set; }
        public decimal? RentAmount { get; set; }
        public bool isAllocatedToGirls { get; set; } = false;
        public string allocatedTo { get; set; } = "Boys";
        public string backgroundColour { get; set; } = "skyblue";
        public Int64? floorId { get; set; }
    }
}
