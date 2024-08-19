namespace InvesTechPlanner.Entities
{
    public class DemandDetail
    {
        public int? DemandDetailsId { get; set; }
        public int? DemandId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? SpendDept { get; set; }
        public string? ExpenseType { get; set; }
        public string? SpendCategory { get; set; }
        public string? CostType { get; set; }
        public decimal? CurrentCost { get; set; }
        public string? ScenarioType { get; set; }
        public decimal? Year0 { get; set; }
        public decimal? Year1 { get; set; }
        public decimal? Year2 { get; set; }
        public decimal? Year3 { get; set; }
        public decimal? Year4 { get; set; }
        public decimal? Year5 { get; set; }
        public DateTime? DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateUpdated { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public bool? IsInactive { get; set; }
        public string? Remarks { get; set; }
    }
}
