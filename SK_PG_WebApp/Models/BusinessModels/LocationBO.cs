using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SK_PG_WebApp.Models.BusinessModels
{
    [Table("locations")]
    public class LocationBO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 id { get; set; }
        public string name { get; set; }
        public Int64? cityId { get; set; }
        public Int64? stateId { get; set; }
        public Int64? countryId { get; set; }
    }
}
