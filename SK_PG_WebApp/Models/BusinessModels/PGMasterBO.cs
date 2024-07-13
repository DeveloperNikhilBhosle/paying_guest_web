using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SK_PG_WebApp.Models.BusinessModels
{
    [Table("pgMaster")]
    public class PGMasterBO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 id { get; set; }
        public string name { get; set; }
        public Int64? locationId { get; set; }
        public Int64? noOfRooms { get; set; } = 0;
        public Int64? noOfBoysRooms { get; set; } = 0;
        public Int64? noOfGirlsRooms { get; set; } = 0;
        public Int64? noOfRoomsBookedBoys { get; set; } = 0;
        public Int64? partiallyBookedBoys { get; set; } = 0;
        public Int64? noOfRoomsAvailableBoys { get; set; } = 0;
        public Int64? noOfRoomsBookedGirls { get; set; } = 0;
        public Int64? partiallyBookedGirls { get; set; } = 0;
        public Int64? noOfRoomsAvailableGirls { get; set; } = 0;
        public string ownerName { get; set; }
        public string ownerAddress { get; set; }
        public string ownerContactNumber { get; set; }
        public string description { get; set; }
        public Int64? cityId { get; set; }
        public Int64? stateId { get; set; }
    }
}
