using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SK_PG_WebApp.Models.BusinessModels
{
    [Table("userNotice")]
    public class UserNoticeBO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 id { get; set; }
        public Int64 userId { get; set; }
        public DateTime lastDate { get; set; }
        public DateTime createdAt { get; set; }
        public bool status { get; set; } = true;
        public string reason { get; set; } = string.Empty;
    }
}
