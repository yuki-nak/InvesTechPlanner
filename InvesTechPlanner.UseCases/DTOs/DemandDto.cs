using System.ComponentModel.DataAnnotations;

namespace InvesTechPlanner.UseCases.DTOs
{
    public class DemandDto
    {
        public int DemandId { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Demand Category is required.")]
        public string DemandCategory { get; set; } = string.Empty;

        [Required(ErrorMessage = "Requested Department is required.")]
        public string RequestedDept { get; set; } = string.Empty;

        [Required(ErrorMessage = "Demand Owner is required.")]
        public string DemandOwner { get; set; } = string.Empty;

        [Required(ErrorMessage = "Requested By is required.")]
        public string RequestedBy { get; set; } = string.Empty;
        public string Objective { get; set; } = string.Empty;
        public string Proposal { get; set; } = string.Empty;
        public string InvestmentDetails { get; set; } = string.Empty;
        public string BusinessImpactIfNotConducted { get; set; } = string.Empty;
        public string RiskComment { get; set; } = string.Empty;
        public string? DemandPriority { get; set; }
        public string? Assumption { get; set; }
        public string? Dependency { get; set; }
        public string? Status { get; set; }
        public string? InvestClassification { get; set; }
        public string? InvestmentScale { get; set; }
        public string? PMOResponsible { get; set; }
        public string? ITBizPartner { get; set; }
        public DateTime? PlannedStart { get; set; }
        public DateTime? PlannedEnd { get; set; }
        public string? FiscalYear { get; set; }
        public string? CreatedBy { get; set; }
        public bool? IsInactive { get; set; } = false;
        public string? Remarks { get; set; }
        public string? DocUrl { get; set; }
    }
}
