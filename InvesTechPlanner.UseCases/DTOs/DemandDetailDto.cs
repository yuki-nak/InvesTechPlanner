using System.ComponentModel.DataAnnotations;

namespace InvesTechPlanner.UseCases.DTOs
{
    public class DemandDetailDto
    {
        [Required(ErrorMessage = "Demand is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Demand.")]
        public int DemandId { get; set; } = 0;
        public int? DemandDetailsId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string? Title { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Spend Department is required.")]
        public string? SpendDept { get; set; }

        [Required(ErrorMessage = "Expense Type is required.")]
        public string? ExpenseType { get; set; }

        [Required(ErrorMessage = "Spend Category is required.")]
        public string? SpendCategory { get; set; }

        [Required(ErrorMessage = "Cost Type is required.")]
        public string? CostType { get; set; }
        [Required(ErrorMessage = "Scenario Type is required.")]
        public string? ScenarioType { get; set; }
        public decimal? Year0 { get; set; }
        public decimal? Year1 { get; set; }
        public decimal? Year2 { get; set; }
        public decimal? Year3 { get; set; }
        public decimal? Year4 { get; set; }
        public decimal? Year5 { get; set; }
        public string? CreatedBy { get; set; }
        public bool? IsInactive { get; set; } = false;
        public string? Remarks { get; set; }
    }
}
