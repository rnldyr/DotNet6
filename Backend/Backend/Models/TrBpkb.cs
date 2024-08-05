using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models
{
    [Table("tr_bpkb")]
    public partial class TrBpkb
    {
        [Key]
        [Column("agreement_number")]
        [StringLength(100)]
        [Unicode(false)]
        public string AgreementNumber { get; set; } = null!;
        [Column("bpkb_no")]
        [StringLength(100)]
        [Unicode(false)]
        public string? BpkbNo { get; set; }
        [Column("branch_id")]
        [StringLength(10)]
        [Unicode(false)]
        public string? BranchId { get; set; }
        [Column("bpkb_date", TypeName = "datetime")]
        public DateTime? BpkbDate { get; set; }
        [Column("faktur_no")]
        [StringLength(100)]
        [Unicode(false)]
        public string? FakturNo { get; set; }
        [Column("faktur_date", TypeName = "datetime")]
        public DateTime? FakturDate { get; set; }
        [Column("location_id")]
        [StringLength(10)]
        [Unicode(false)]
        public string? LocationId { get; set; }
        [Column("police_no")]
        [StringLength(20)]
        [Unicode(false)]
        public string? PoliceNo { get; set; }
        [Column("bpkb_date_in", TypeName = "datetime")]
        public DateTime? BpkbDateIn { get; set; }
        [Column("created_by")]
        [StringLength(20)]
        [Unicode(false)]
        public string? CreatedBy { get; set; }
        [Column("created_on", TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        [Column("last_updated_by")]
        [StringLength(20)]
        [Unicode(false)]
        public string? LastUpdatedBy { get; set; }
        [Column("last_updated_on", TypeName = "datetime")]
        public DateTime? LastUpdatedOn { get; set; }

        [ForeignKey("LocationId")]
        public virtual MsStorageLocation? Location { get; set; }
    }
}
