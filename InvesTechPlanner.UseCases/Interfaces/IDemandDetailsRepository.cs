using InvesTechPlanner.Entities;
using InvesTechPlanner.UseCases.DTOs;

namespace InvesTechPlanner.UseCases.Interfaces
{
    public interface IDemandDetailsRepository
    {
        Task Add(DemandDetail demandDetail);
        Task Update(DemandDetail demandDetail);
        Task Delete(int demandDetailsId);
        Task<DemandDetail?> GetById(int demandDetailsId);
        Task<IEnumerable<DemandDetail>> GetDetailedDemandID(int demandId);
        Task<IEnumerable<DemandDetail>> GetDemandDetailsByDemandId(int demandId);
        Task<MemoryStream> ExportDemandDetailsToCsv(int demandId);
        Task<Dictionary<string, Dictionary<string, Dictionary<string, SummaryDto>>>> GetSummaryByScenarioAndCostType(int demandId);
    }
}
