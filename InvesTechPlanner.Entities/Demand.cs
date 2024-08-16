namespace InvesTechPlanner.Entities
{
    public class Demand
    {
        public int DemandId { get; set; }
        public string? Title { get; set; }
        public string? RequestedDept { get; set; }
        public string? RequestedBy { get; set; }
        public string? DemandOwner { get; set; }
        public string? DemandCategory { get; set; }
        public string? Objective { get; set; }
        public string? Proposal { get; set; }
        public string? InvestmentDetails { get; set; }
        public string? BusinessImpactIfNotConducted { get; set; }
        public string? RiskComment { get; set; }
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
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateUpdated { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public bool? IsInactive { get; set; }
        public string? Remarks { get; set; }
    }
}
