using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SK_PG_WebApp.Models.BusinessModels
{
    [Table("PayingGuistMaster")]
    public class PayingGuestMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 id { get; set; }
        public Int64 userId { get; set; }
        public decimal? depositAmount { get; set; }
        public decimal? rentAmount { get; set; }
        public Int64 pgRoomMasterId { get; set; }
        public Int64 pgMasterId { get; set; }
        public DateTime activatedDate { get; set; } = DateTime.Now;
        public DateTime? lastRenewDate { get; set; } = DateTime.Now;
        public DateTime? nextRenewDate { get; set; } = DateTime.Now;
        public Int64? agreementMonths { get; set; }
        public DateTime? rentPayableDate { get; set; }
        public Int64? floorId { get; set; }

    }
}
