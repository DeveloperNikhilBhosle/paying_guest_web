using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SK_PG_WebApp.Models.BusinessModels
{
    [Table("propertyFloor")]
    public class PropertyFloorBO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 id { get; set; }
        public string name { get; set; }
        public Int64 pgMasterId { get; set; }
        public bool isActive { get; set; } = true;
        public string allocatedTo { get; set; }
        public bool isAllocatedToGirls { get; set; }
    }
}
