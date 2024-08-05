using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models
{
    [Table("ms_storage_location")]
    public partial class MsStorageLocation
    {
        [Key]
        [Column("location_id")]
        [StringLength(10)]
        [Unicode(false)]
        public string LocationId { get; set; } = null!;
        [Column("location_name")]
        [StringLength(100)]
        [Unicode(false)]
        public string? LocationName { get; set; }
    }
}
