using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SK_PG_WebApp.Models.BusinessModels
{
    [Table("payingguestpayment")]
    public class PayingGuestPaymentBO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 id { get; set; }
        public Int64 userId { get; set; }
        public Int64 payToUserId { get; set; }
        public decimal amount { get; set; }
        public Int64 paymentTypeId { get; set; }
        public string paymentDescription { get; set; }
        public Int64 pgRoomMasterId { get; set; }
        public Int64 pgMasterId { get; set; }
        public DateTime paymentDate { get; set; }
        public bool isActive { get; set; } = true;
        public decimal discount { get; set; }
        public bool isInvoiceGenerated { get; set; } = false;
        public string paymentDateRange { get; set; }
    }
}
