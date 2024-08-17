using InvesTechPlanner.Entities;

namespace InvesTechPlanner.UseCases.Interfaces
{
    public interface IDemandDetailsRepository
    {
        Task Add(DemandDetail demandDetail);
        Task Update(DemandDetail demandDetail);
        Task Delete(int demandDetailsId);
        Task<DemandDetail?> GetById(int demandDetailsId);
        Task<IEnumerable<DemandDetail>> GetDetailedDemandID(int demandId);
    }
}
