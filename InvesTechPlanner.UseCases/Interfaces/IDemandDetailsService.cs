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
    }
}
