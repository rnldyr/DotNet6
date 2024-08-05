using System.ComponentModel.DataAnnotations;

namespace Frontend.Models{
    public class TrBpkb
    {
        [Required(ErrorMessage = "Agreement Number is required.")]
        public string AgreementNumber { get; set; }

        public string? BpkbNo { get; set; }
        public string? BranchId { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BpkbDate { get; set; }

        public string? FakturNo { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FakturDate { get; set; }

        public string? LocationId { get; set; }
        public string? PoliceNo { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BpkbDateIn { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
    }
}
