using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SK_PG_WebApp.Models.BusinessModels
{
    [Table("userDetails")]
    public class UserDetailsBO
    {
        [Key]
        public Int64 userId { get; set; }
        public string qualification { get; set; }
        public string education { get; set; }
        public string aadhar { get; set; }
        public string pan { get; set; }
        public Int64? referByUserId { get; set; }
        public string familyContactNumber { get; set; }
        public string familyMember { get; set; }
    }
}
