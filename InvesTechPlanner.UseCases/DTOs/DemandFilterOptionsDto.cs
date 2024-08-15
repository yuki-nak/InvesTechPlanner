namespace InvesTechPlanner.UseCases.DTOs
{
    public class DemandFilterOptionsDto
    {
        public string? FiscalYear { get; set; }
        public string? DemandCategory { get; set; }
        public string? RequestedDept { get; set; }
        public string? InvestmentScale { get; set; }
        public string? DemandPriority { get; set; }

        public List<string?> FiscalYears { get; set; } = new List<string?>();
        public List<string?> DemandCategories { get; set; } = new List<string?>();
        public List<string?> RequestedDepts { get; set; } = new List<string?>();
        public List<string?> InvestmentScales { get; set; } = new List<string?>();
        public List<string?> DemandPriorities { get; set; } = new List<string?>();
    }
}