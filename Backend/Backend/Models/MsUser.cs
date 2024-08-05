using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models
{
    [Table("ms_user")]
    public partial class MsUser
    {
        [Key]
        [Column("user_id")]
        public long UserId { get; set; }
        [Column("user_name")]
        [StringLength(20)]
        [Unicode(false)]
        public string? UserName { get; set; }
        [Column("password")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Password { get; set; }
        [Column("is_active")]
        public bool? IsActive { get; set; }
    }
}
