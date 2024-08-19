using InvesTechPlanner.UseCases.DTOs;

namespace InvesTechPlanner.UseCases.Interfaces
{
    public interface IDemandDetailsService
    {
        Task CreateDemandDetail(DemandDetailDto demandDetailDto);
        Task UpdateDemandDetail(DemandDetailDto demandDetailDto);
        Task DeleteDemandDetail(int demandDetailsId);
        Task<IEnumerable<DemandDetailDto>> GetDetailedDemandID(int demandDetailsId);
        Task<IEnumerable<DemandDetailDto>> GetDemandDetailsByDemandId(int demandId);
        Task<MemoryStream> ExportDemandDetailsToCsv(int demandId);
        Task<Dictionary<string, Dictionary<string, Dictionary<string, SummaryDto>>>> GetSummaryByScenarioAndCostType(int demandId);
        decimal? CalculatePaybackPeriod(SummaryDto investedSummary, SummaryDto currentSummary);

    }
}
