using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SK_PG_WebApp.Models.BusinessModels
{
    [Table("adminPropertyMapping")]
    public class AdminPropertyMappingBO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 id { get; set; }
        public Int64 userId { get; set; }
        public Int64 pgmasterId { get; set; }
        public bool isActive { get; set; } = true;
    }
}
