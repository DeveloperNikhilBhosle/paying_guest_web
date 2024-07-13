using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SK_PG_WebApp.Models.BusinessModels
{
    [Table("Users")]
    public class UsersBO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 id { get; set; }
        public string name { get; set; }
        public string designation { get; set; }
        public string email { get; set; }
        public string password { get; set; } = "12345";
        public string location { get; set; }
        public Int64 roleId { get; set; }
        public DateTime? lastLoginDate { get; set; }
        public DateTime created_date { get; set; } = DateTime.Now;
        public Int64? contactNumber { get; set; }
        public Int64? whatsAppNumber { get; set; }
        public DateTime? DOB { get; set; }
        public bool isActive { get; set; } = true;
        public string about { get; set; }
        public string address { get; set; }
        public string Gender { get; set; }
        public string photo { get; set; }
    }
}
