using InvesTechPlanner.UseCases.DTOs;

namespace InvesTechPlanner.UseCases.Interfaces
{
    public interface IDemandService
    {
        Task CreateDemand(DemandDto demandDto);
        Task DeleteDemand(int demandId);
        Task<List<DemandDto>> GetDemandListItems();
        Task<DemandFilterOptionsDto> GetOptionsForFilters();
        Task<List<DemandDto>> GetFilteredDemands(DemandFilterOptionsDto filterOptions);
        Task<MemoryStream> ExportDemandsToCsv();
    }
}
